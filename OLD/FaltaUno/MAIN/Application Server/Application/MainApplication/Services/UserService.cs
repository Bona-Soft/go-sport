using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using static MYB.FaltaUno.Application.MainApplication.Services.Services;

namespace MYB.FaltaUno.Application.MainApplication.Services
{
	public static class UserService
	{
	
		public static IUser EditPersonalInfo(IUser user)
		{
			if (GetCurrentUser().UserID != user.UserID)
			{
				BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("EditPersonalInfo", "UsersNotMatch", "You are not allowed to edit this user"));
				return null;
			}
			Save(user);

			return GetCurrentUser();
		}

		public static bool Login(string username, string password, string ip, string userAgent, bool remember = false)
		{
			return BaseApp.Session().Login(username, password, ip, userAgent, remember);
		}

		public static bool ChangeUserPassword(string currentPassword, string newPassword)
		{
			IUser user = UserService.GetCurrentUser();

			if (!user.Base.CheckPassword(currentPassword))
			{
				BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("ChangePassword", "OldPasswordNotMatch", "Your current password does not match with the saved password"));
				return false;
			}

			return user.Base.ChangePassword(newPassword);
		}

		#region GetUser

		public static IUser GetCurrentUser() => GetUser();

		public static IUser GetUser()
		{
			IUser user = UserFactory.CurrentUser();
			if (user == null)
			{
				throw new Exception("User is not logged in");
			}
			else if (user.Initialized)
			{
				return user;
			}

			user = Services.UserRepoService.GetCurrent();

			short? sportID = user.LastActiveSport == null ? MainService.GetActiveSport() : user.LastActiveSport.SportID;
			if (sportID != null)
				user.LastActiveSport = SportService.GetSport((short)sportID);

			UserService.LoadPrivacy(user);

			FillLists(user);
			return user;
		}

		#region UserPrivacy

		public static IUserPrivacy GetUserPrivacy(IUser user) => GetUserPrivacy(user.UserID);

		public static IUserPrivacy GetUserPrivacy(long userID)
		{
			return UserRepoService.GetPrivacy(userID);
		}

		private static IUserPrivacy _DefaultUserPrivacy { get; set; }

		public static IUserPrivacy GetDefaultPrivacy()
		{
			if (_DefaultUserPrivacy == null)
			{
				_DefaultUserPrivacy = UserRepoService.GetDefaultPrivacy();
			}
			return _DefaultUserPrivacy;
		}

		private static void LoadPrivacy(IUser user)
		{
			user.UserPrivacy = GetUserPrivacy(user);
		}

		#endregion UserPrivacy

		private static void FillLists(IUser user)
		{
			user.Players = PlayerService.GetPlayers(user);
		}

		public static IUser GetUser(long userID)
		{
			IUser user = UserFactory.User(userID);
			if (user != null && user.Initialized)
			{
				return user;
			}

			user = Services.UserRepoService.GetByID(userID);

			user.UserPrivacy = UserService.GetUserPrivacy(userID);
			FillLists(user);

			return user;
		}

		public static bool IsUserPrivacyShowable(IUser user, string entity, string property, PrivacyValue value)
		{
			IUser loggedUser = GetCurrentUser();
			if (loggedUser != null && user.UserID == loggedUser.UserID)
			{
				return true;
			}
			return value == PrivacyValue.Everybody || (value == PrivacyValue.Partial && ShareAnyTeamOrMatch(user));
		}

		//   public static void CheckUserPrivacy(IUser user )
		//   {
		//if(user.UserPrivacy != null)
		//{
		//	foreach(var entity in user.UserPrivacy)
		//	{
		//	}
		//}

		//      if (user.UserPrivacy != null && user.UserPrivacy.MobileNumber != null
		//         && (user.UserPrivacy.MobileNumber.Contains(PrivacyValue.Nobody.ToString())
		//         || (user.UserPrivacy.MobileNumber.Contains(PrivacyValue.Partial.ToString())
		//            && !ShareAnyTeamOrMatch(user))))
		//      {
		//         user.MobileNumber = null;
		//      }

		//      if (user.UserPrivacy != null && user.UserPrivacy.BirthDate != null
		//         && (user.UserPrivacy.BirthDate.Contains(PrivacyValue.Nobody.ToString())
		//         || (user.UserPrivacy.BirthDate.Contains(PrivacyValue.Partial.ToString())
		//            && !ShareAnyTeamOrMatch(user))))
		//      {
		//         user.BirthDate = null;
		//      }
		//   }

		private static bool ShareAnyTeamOrMatch(IUser user)
		{
			List<IMatch> currentUserMatches = MatchService.GetCurrentUserMatches();
			List<IMatch> userMatches = MatchService.GetUserMatches(user);
			return currentUserMatches.Where(b => userMatches.Any(a => a.MatchID == b.MatchID)).Count() > 0;
		}

		public static IUser GetUserByEmail(string mail)
		{
			IUser user = Services.UserRepoService.GetByEmail(mail);
			FillLists(user);
			return user;
		}

		#endregion GetUser

		public static long RegisterUser(string mail, string password, string name, string lastName, short sportID)
		{
			string verificationCode;
			long userID = -1;

			try
			{
				BaseApp.BaseRepoService.BeginTransaction("RegisterUser");

				userID = BaseApp.BaseUserManager.RegisterUserPendingVerify(mail, password, name, lastName, out verificationCode);

				switch (userID)
				{
					case -1:
						verificationCode = BaseApp.BaseUserManager.GetUserVerificationCode(mail);
						MailManager.SendUserVerificationEmail(mail, verificationCode);
						BaseApp.BaseRepoService.RollbackTransaction("RegisterUser");
						return userID;
				}

				IUser currentCreatedUser = GetUser(userID);
				ISport sport = SportService.GetSport(sportID);

				currentCreatedUser.DefaultSport = sport;
				currentCreatedUser.LastActiveSport = sport;
				currentCreatedUser.UserPrivacy = CreateUserPrivacy(currentCreatedUser);

            UserService.Save(currentCreatedUser);

            PlayerService.CreateDefaultPlayer(currentCreatedUser, sport);
				
				BaseApp.BaseRepoService.CommitTransaction("RegisterUser");
			}
			catch (Exception ex)
			{
				BaseApp.BaseRepoService.RollbackTransaction("RegisterUser");
				throw ex;
			}

			MailManager.SendUserVerificationEmail(mail, verificationCode);

			return userID;
		}

		private static IUserPrivacy CreateUserPrivacy(IUser user)
		{
			IUserPrivacy up = UserRepoService.GetPrivacy(user.UserID);
			up = SavePrivacy(up, user);

			return up;
		}

		private static long Save(IUser user)
		{
			if (user.UserID > 0)
			{
				user.Initialized = false;
			}
			return Services.UserRepoService.Save(user);
		}

		public static ISport SetDefaultSport(IUser user, short sportID)
		{
			user.DefaultSport = SportService.GetSport(sportID);
			Save(user);
			return user.DefaultSport;
		}

		public static void SetActiveSport(IUser user, short sportID)
		{
			user.LastActiveSport = SportService.GetSport(sportID);
			Save(user);
		}

		public static ISport GetActiveSport(IUser user)
		{
			return user.LastActiveSport;
		}

		public static ISport GetActiveSport()
		{
			if (GetCurrentUser() != null)
				return GetCurrentUser().LastActiveSport;
			return null;
		}

		public static IUserPrivacy GetCurrentUserPrivacy()
		{
			return GetCurrentUser().UserPrivacy;
		}

		public static short VerifyUser(string emailAddress, string verificationCode)
		{
			short result;

			try
			{
				Services.BaseRepoService.BeginTransaction("VerifyUser");
				result = BaseApp.BaseUserManager.VerifyUserEmail(emailAddress, verificationCode);
				if (result < 0)
				{
					BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("VerifyUser", "UnknownErrorVerifing", "There was an error during your verification"));
				}
				Services.BaseRepoService.CommitTransaction("VerifyUser");
			}
			catch (Exception ex)
			{
				Services.BaseRepoService.RollbackTransaction("VerifyUser");
				throw ex;
			}

			return result;
		}

		public static string UpdateAvatar(Image avatar, string fileName)
		{
			bool result = false;
			bool uploaded = false;
			string path = HttpContext.Current.Server.MapPath("~\\UploadedFiles\\Avatars\\" + GetCurrentUser().UserID.Value.ToString());
			string thumbnailPath = path + "\\Thumbnails";

			string oldAvatar = GetCurrentUser().Avatar;

			fileName = BaseApp.FileManager.GetRandomFileName(fileName);

			try
			{
				uploaded = BaseApp.FileManager.SaveFile(path, avatar, fileName);

				if (uploaded)
				{
					using (Image thumbnail = ImageTools.CroppedThumbnailFromImage(avatar, 100, 100, avatar.RawFormat, ImageTools.CroppedFromPosition.TOP_CENTER))
					{
						uploaded = BaseApp.FileManager.SaveFile(thumbnailPath, thumbnail, fileName);
					}

					if (uploaded)
					{
						result = SaveAvatar(GetCurrentUser(), fileName);

						if (oldAvatar != null && result)
						{
							BaseApp.FileManager.DeleteFile(path + "\\" + oldAvatar);
							BaseApp.FileManager.DeleteFile(thumbnailPath + "\\" + oldAvatar);
						}

						GetCurrentUser().Avatar = fileName;

						return fileName;
					}
				}
			}
			catch (Exception ex)
			{
				GetCurrentUser().Avatar = oldAvatar;
				try
				{
					BaseApp.FileManager.DeleteFile(path + "\\" + fileName);
				}
				catch { }
				try
				{
					BaseApp.FileManager.DeleteFile(thumbnailPath + "\\" + fileName);
				}
				catch { }

				throw ex;
			}

			BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("UploadAvatar", "UploadFileAvatarError", "File could not be saved"));
			return String.Empty;
		}

		public static bool SaveAvatar(IUser user, string newFileName)
		{
			bool result = Services.UserRepoService.SaveAvatar(user.UserID, newFileName);
			BaseApp.BaseUser().Release();
			return result;
		}

		private static IUserPrivacy SavePrivacy(IUserPrivacy up, IUser user)
		{
			return Services.UserRepoService.SavePrivacy(up, user);
		}

		public static IUserPrivacy EditPrivacy(IUserPrivacy up)
		{
			IUser user = UserService.GetCurrentUser();
			foreach (var entity in user.UserPrivacy)
			{
				if (!up.ContainsKey(entity.Key))
				{
					BaseApp.ErrorManager.AddError("The privacy was already modified. Reload the page and try again");
					return up;
				}

				foreach (var prop in (Dictionary<long, string, PrivacyValue>)entity.Value)
				{
					if (!up[entity.Key].ContainsKey(prop.Key.Item1, prop.Key.Item2))
					{
						BaseApp.ErrorManager.AddError("The privacy was already modified. Reload the page and try again");
						return up;
					}
				}
			}
			user.UserPrivacy = SavePrivacy(up, user);
			return user.UserPrivacy;
		}

		//TODO: Pasar al base, crear opciones, FileManager
		/*
      private static string GetRandomFileName(HttpPostedFile originalFile) => GetRandomFileName(originalFile.FileName);

      private static string GetRandomFileName(string originalFileName)
      {
         string fileName = originalFileName.ToNormalized().Replace(".","").Replace(" ","").ToRandom();
         fileName += GetExtension(originalFileName);
         return fileName;
      }

      private static string GetExtension(HttpPostedFile file) => GetExtension(file.FileName);

      private static string GetExtension(string fileName)
      {
         return Path.GetExtension(fileName).ToLower();
      }

      private static bool DeleteFile(string filePath)
      {
         string path = "~\\" + filePath;
			//TODO: revisar mappath y directory from.
         path = HttpContext.Current.Server.MapPath(path);

         if (File.Exists(path))
         {
            File.Delete(path);
         }

         return !File.Exists(path);
      }

      private static bool UploadFile(string folderName, HttpPostedFile file) => UploadFile(folderName, file, file.FileName);

      private static bool UploadFile(string folderName, HttpPostedFile file, string fileName, bool overwrite = true)
      {
         // make folder path
         string _folderPath = "~\\" + folderName;

         // create folder directory info
         DirectoryInfo FolderDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(_folderPath));

         // check if folder directory not exist
         if (!FolderDir.Exists)
         {
            // create directory
            FolderDir.Create();
         }

         // define file path
         string _filePath = Path.Combine(HttpContext.Current.Server.MapPath(_folderPath), fileName);

         // check if file not exist
         if (!File.Exists(_filePath) || overwrite)
         {
            // save file into folder directory
            file.SaveAs(_filePath);
         }

         return File.Exists(_filePath);
      }
      */
	}
}