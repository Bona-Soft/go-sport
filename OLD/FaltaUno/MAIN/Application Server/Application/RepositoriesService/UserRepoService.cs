using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{
	public class UserRepoService : RepoServices, IUserRepositoryService
	{
		public bool SaveAvatar(long userID, string fileName)
		{
			return UserRepo.UpdateAvatar(userID, fileName, BaseRepoService.CurrentTransaction);
		}

		#region "User Privacy"

		public IUserPrivacy GetPrivacy(long userID)
		{
			DataTable dt = UserRepo.GetPrivacy(userID);
			IUserPrivacy up = UserFactory.NewUserPrivacy(userID);
			Fill(up, dt);
			return up;
		}

		public IUserPrivacy GetDefaultPrivacy()
		{
			DataTable dt = UserRepo.GetDefaultPrivacy();
			IUserPrivacy up = UserFactory.NewUserPrivacy();
			Fill(up, dt);
			return up;
		}

		public IUserPrivacy SavePrivacy(IUserPrivacy up, IUser user)
		{
			IUserPrivacy aux = (IUserPrivacy)up.Clone();

			foreach (var entity in up)
			{
            aux[entity.Key] = entity.Value.Clone();
				foreach (var prop in (Dictionary<long,string,PrivacyValue>)entity.Value)
				{
					if (prop.Key.Item1 == 0) //item1 = UserPrivacyID
					{
						long newUpID = UserRepo.AddPrivacy(user.UserID, entity.Key, prop.Key.Item2, prop.Value.ToDefType<char>(), BaseRepoService.CurrentTransaction);
						aux.Remove(entity.Key, prop.Key.Item2);
						aux.Add(newUpID, entity.Key, prop.Key.Item2, prop.Value);
					}
					else
					{
						UserRepo.UpdatePrivacy(prop.Key.Item1, user.UserID, prop.Value.ToDefType<char>(), BaseRepoService.CurrentTransaction);
					}
				}
			}

			return aux;
		}

		#endregion "User Privacy"

		#region "User entity"

		public IUser GetCurrent()
		{
			IUser user = UserFactory.CurrentUser();
			Fill(user, UserRepo.Get);

			return user;
		}

		public long Save(IUser user)
		{
			Dictionary<string, Type, object> dic = Key.EntityToDictionary(user);
			dic.Rename("SportID", "DefaultSportID");
			if (user.LastActiveSport != null)
				dic.Add("LastActiveSportID", typeof(short), user.LastActiveSport.SportID.Value);
			if (user.UserID > 0)
				return UserRepo.Update(dic, BaseRepoService.CurrentTransaction);
			dic.Remove<long>("UserID");
			//TODO: llamar al crear user de base
			return UserRepo.Add(dic, BaseRepoService.CurrentTransaction);
		}

		public IUser GetByID(long userID)
		{
			IUser user = UserFactory.User(userID);
			Fill(user, UserRepo.Get);

			return user;
		}

		public IUser GetByEmail(string email)
		{
			DataTable dt = UserRepo.GetByEmail(email);
			IUser user;
			if (dt.Rows.Count > 0)
			{
				user = UserFactory.User(dt.Rows[0]["UserID"].ToDefType<long>());
				Fill(user, dt);
				return user;
			}
			throw new Exception("User can not be found");
		}

		#endregion "User entity"

		#region "Fills Entity Methods"

		private void Fill(IUser user, Func<long, DataTable> getFunc)
		{
			if (user != null)
			{
				DataTable dt = getFunc(user.UserID);
				Fill(user, dt);
			}
		}

		private void Fill(IUser user, DataTable dt)
		{
			if (dt == null || dt.Rows.Count == 0)
				throw new Exception("User can not be found");
			DataRow dr = dt.Rows[0];
			Key.FillEntity(user, dr);	

         user.DefaultSport = SportFactory.NewDef(dr["DefaultSportID"], null);
			user.LastActiveSport = SportFactory.NewDef(dr["LastActiveSportID"], null);
			user.UserPrivacy = null;
			user.Init();
		}

		private IUserPrivacy Fill(IUserPrivacy up, DataTable dt)
		{
			if (dt == null || dt.Rows.Count == 0)
				throw new Exception("User privacy can not be found");
			DataRow dr = dt.Rows[0];

			foreach (DataRow row in dt.Rows)
			{
				up.Add(row["UserPrivacyID"].ToDefType<long>(), row["Entity"].ToDefString(), row["Property"].ToDefString(), row["Value"].ToDefEnumChar<PrivacyValue>());
			}

			return up;
		}

		#endregion "Fills Entity Methods"
	}
}