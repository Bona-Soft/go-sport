using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Cryptography;
using MYB.BaseApplication.Framework.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MYB.BaseApplication.Domain.Users
{
	public class BaseUserManager : IBaseUserManager
	{
		#region " Shortcut Properties "

		private IDataBase DB
		{
			get
			{
				return BaseApp.DB;
			}
		}

		private IWSP WSP
		{
			get
			{
				return BaseApp.WSP;
			}
		}

		#endregion " Shortcut Properties "

		public BaseUserManager()
		{
		}

		private void ReleaseUser(long userID)
		{
			BaseApp.BaseUser(userID).Release();
		}

		public long CreateUser(string username, string password)
		{
			return WSP.UserGroup.AddUser(username, password.ToHash()).Execute(DB);
		}

		public string GetUserVerificationCode(string emailAddress)
		{
			return WSP.UserGroup.GetUserVerificationCode(emailAddress).Execute(DB);
		}

		public long RegisterUserPendingVerify(string emailAddress, string password, out string verificationCode) => RegisterUserPendingVerify(emailAddress, password, null, null, out verificationCode);

		public long RegisterUserPendingVerify(string emailAddress, string password, string name, string lastName , out string verificationCode)
		{
			verificationCode = (GuidGenerator.GetString() + emailAddress).ToHash();
			verificationCode = Regex.Replace(verificationCode, @"[#|$|%|&|@|`|/|:|;|<|=|>|?|\[|\\|\]|^|{|}|~|“|‘|+|,|\|]", "");
			long userID = WSP.UserGroup.AddUser(emailAddress, password.ToHash(), name, lastName, verificationCode).Execute(DB);
			return userID;
		}

		public short VerifyUserEmail(string emailAddress, string verificationCode)
		{
			return WSP.UserGroup.VerifyUserEmail(emailAddress, verificationCode).Execute(DB);
		}

		public bool RemoveUser(long userID)
		{
			if (WSP.UserGroup.RemoveUser(userID).Execute(DB))
			{
				ReleaseUser(userID);
				return true;
			}
			return false;
		}

		public bool DeleteUser(long userID)
		{
			if (WSP.UserGroup.DeleteUser(userID).Execute(DB))
			{
				ReleaseUser(userID);
				return true;
			}
			return false;
		}

		public bool DisconnectUser(long userID, bool clearSessions = false)
		{
			if (WSP.UserGroup.DisconnectUser(userID, clearSessions).Execute(DB))
			{
				ReleaseUser(userID);
				if (clearSessions)
				{
					WSP.UserGroup.RemoveAllUserSession(userID).Execute(DB);
				}
				return true;
			}
			return false;
		}

		public bool DeleteLogin(string username)
		{
			return WSP.UserGroup.DeleteLogin(username).Execute(DB);
		}

		public List<IBaseUser> GetAllActiveUsers()
		{
			return BaseApp.ContainerManager.ResolveAll<IBaseUser>().ToList();
		}
	}
}