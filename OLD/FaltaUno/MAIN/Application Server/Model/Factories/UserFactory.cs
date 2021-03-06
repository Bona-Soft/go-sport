using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Model.Factories
{
	public class UserFactory : IUserFactory
	{
		public IUser CurrentUser()
		{
			if (BaseApp.Session().SessionString == null || BaseApp.Session().User == null || BaseApp.Session().User.UserID <= 0)
			{
				return null;
			}
			return User(BaseApp.Session().User.UserID);
		}

		public IUser User(long userID)
		{
			Dictionary<string, long> constructParameters = new Dictionary<string, long>
			{
				{ "userID", userID }
			};
			return BaseApp.Resolve<IUser>(constructParameters);
		}

		public IUserPrivacy NewUserPrivacy()
		{
			return new UserPrivacy(0);
		}
		public IUserPrivacy NewUserPrivacy(long userID)
		{
			return new UserPrivacy(userID);
		}
	}
}