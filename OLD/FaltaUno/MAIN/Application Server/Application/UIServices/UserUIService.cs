using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.MainApplication.Services;
using MYB.FaltaUno.Model.Interfaces.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.Application.UIService
{
	public class UserUIService : UIServices
	{
		#region Instance

		private static UserUIService m_Instancia;

		public static UserUIService Instancia
		{
			get
			{
				if (m_Instancia == null)
				{
					m_Instancia = new UserUIService();
				}
				return m_Instancia;
			}
		}

		public UserUIService()
		{
		}

		#endregion Instance

		public bool Login(string username, string password, string ip, string userAgent, bool remember = false)
			=> UserService.Login(username, password, ip, userAgent, remember);

		public JObject GetLoggedUser() => GetLoggedUser(UIView.Short);

		public JObject GetLoggedUser(UIView uiView)
		{
			JObject userView = new JObject();
			return GetLoggedUser(userView, uiView);
		}

		public JObject GetLoggedUser(JObject userView, UIView uiView = UIView.Full)
		{
			//TODO: Convertir User en Entity, agregarle key y utilizar
			//  ObjToJObj(UserService.GetCurrentUser(), uiView);

			IUser user = UserService.GetCurrentUser();
			userView["User"] = user.ToJObject(uiView);
			userView["User"]["ActiveSport"] = user.LastActiveSport.ToJObject(uiView);
			userView["User"]["DefaultSport"] = user.DefaultSport.ToJObject(uiView);

			return userView;
		}

		public short VerifyUser(string emailAddress, string verificationCode)
		{
			return UserService.VerifyUser(emailAddress, verificationCode);
		}

		public long RegisterUser(string mail, string password, string name, string lastName, short sportID)
			=> UserService.RegisterUser(mail, password, name, lastName, sportID);

		public JObject GetUser(long userID)
		{
			JObject userView = new JObject();
			userView["User"] = UserService.GetUser(userID).ToJObject(UIView.Full);
			return userView;
		}

		public void SetActiveSport(short sport)
		{
			UserService.SetActiveSport(UserService.GetCurrentUser(), sport);
		}

		public JObject GetActiveSport()
		{
			ISport sport = UserService.GetActiveSport(UserService.GetCurrentUser());
			return sport.ToJObject(UIView.Full);
		}

		public string UpdateAvatar(Image avatar, string fileName)
		{
			return UserService.UpdateAvatar(avatar, fileName);
		}

		public JObject EditPrivacy(JObject userPrivacy)
		{
			IUserPrivacy up = UserFactory.NewUserPrivacy(UserService.GetCurrentUser().UserID);

			foreach (var entity in userPrivacy)
			{
				up.Add(entity.Key, new Dictionary<long, string, PrivacyValue>());

				foreach (var prop in entity.Value)
				{
					up[entity.Key].Add(prop["UserPrivacyID"].ToDefType<long>(), prop["Property"].ToDefString(), prop["Value"].ToDefEnumChar<PrivacyValue>());
				}
			}
			up = UserService.EditPrivacy(up);
			return UserPrivacyToJObject(up);
		}

		public JObject GetCurrentUserPrivacy()
		{
			IUserPrivacy up = UserService.GetCurrentUserPrivacy();

			return UserPrivacyToJObject(up);
		}

		public JObject UserPrivacyToJObject(IUserPrivacy up)
		{
			JObject privacy = new JObject();

			foreach (var entity in up)
			{
				privacy[entity.Key] = new JArray();
				foreach (var prop in entity.Value)
				{
					JObject obj = new JObject();
					obj["UserPrivacyID"] = prop.Key.Item1;
					obj["Property"] = prop.Key.Item2;
					obj["Value"] = prop.Value.ToDefType<char>().ToString();
					((JArray)privacy[entity.Key]).Add(obj);
				}
			}

			return privacy;
		}

		public bool ChangeUserPassword(string currentPassword, string newPassword)
		{
			return UserService.ChangeUserPassword(currentPassword, newPassword);

		}

		public JObject EditPersonalInfo(JObject user)
		{
			IUser _user = UserFactory.User(user["UserID"].ToDefType<long>());

			_user = user.ToClass<IUser>(_user);
			_user = UserService.EditPersonalInfo(_user);

			JObject obj = _user.ToJObject();


			//TODO: Corregir ToJObject para que pueda tomar fechas tipo null DateTime?
			if (_user.BirthDate != null)
			{
				obj["BirthDate"] = _user.BirthDate.ToDefType<DateTime>();
			}
				

			return obj;
		}
	}
}