using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Cryptography;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MYB.BaseApplication.Domain.Users
{
	public class BaseUser : IBaseUser
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

		#region " Constructor "

		public BaseUser(long userID, int implementationID = 0)
		{
			_UserID = userID;
			_ImplementationID = implementationID;
			News = 0;
		}

		#endregion " Constructor "

		#region " Private Properties "

		private long _UserID;
		private int _ImplementationID;

		

		#endregion " Private Properties "

		#region " Private Methods "


		private List<ILogins> GetLogins()
		{
			List<ILogins> logins = new List<ILogins>();
			DataSet ds = WSP.UserGroup.EnumUserLogins(UserID).Execute(DB);

			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				ILogins auxLogin = new Logins(
						Convert.ToInt64(ds.Tables[0].Rows[0]["LoginID"]),
						ds.Tables[0].Rows[0]["LoginName"].ToString()
					);
				logins.Add(auxLogin);
			}

			return logins;
		}

		private List<string> GetSessions()
		{
			return WSP.UserGroup.EnumUserSessions(UserID).Execute(DB).RowsToList<string>();
		}

		#endregion " Private Methods "

		#region " Public Properties "

		public int News { get; set; }
		public Dictionary<string, int> HasNewsCollection { get; set; }
		public long UserID
		{
			get
			{
				return _UserID;
			}
		}

		public int ImplementationID
		{
			get
			{
				return _ImplementationID;
			}
		}

		public List<ILogins> Logins
		{
			get
			{
				return GetLogins();
			}
		}

		public List<string> Sessions
		{
			get
			{
				return GetSessions();
			}
		}

		public List<IAppSession> ActiveSessions
		{
			get
			{
				return BaseApp.ContainerManager
							.ResolveAll<IAppSession>()
							.Where(session => session.User.Equals(this))
							.ToList();
			}
		}

		public string DefaultLanguage { get; set; }

		#endregion " Public Properties "

		#region " Public Methods "

		public long AddLogin(string username, string password) => AddLogin(username, new string[] { password });

		public long AddLogin(string username, string[] password)
		{
			long loginID = 0;
			if (WSP.UserGroup.ExistsUsername(username).Execute(DB) == 1)
			{
				foreach (string pass in password)
					loginID = WSP.UserGroup.AddUserLogin(UserID, username, pass).Execute(DB);
			}
			return loginID;
		}

		public bool ChangePassword(string newPassword)
		{
			return WSP.SessionGroup.ChangePassword(UserID, newPassword.ToHash()).Execute(DB) > 0;
		}

		public bool ChangePassword(string loginName, string newPassowrd)
		{

			return WSP.SessionGroup.ChangePassword(UserID, newPassowrd.ToHash(), loginName).Execute(DB) > 0;
		}

		public bool CheckPassword(string loginName, string currentPassword)
		{
			bool passwordMatch = false;

			DataTable dt = WSP.SessionGroup.GetLoginPasswordsByUserID(UserID).Execute(DB);
	
			if (dt.Rows.Count == 0)
				return false;

			if (UserID > 0)
			{

				foreach (DataRow row in dt.Rows)
				{
					if (loginName == row["LoginName"].ToDefString() && HashHelper.Verify(currentPassword, row["Password"].ToDefString()))
					{
						passwordMatch = true;
					}
				}
			}

			return passwordMatch;
		}

		public bool CheckPassword(string currentPassword)
		{
			bool passwordMatch = false;

			DataTable dt = WSP.SessionGroup.GetLoginPasswordsByUserID(UserID).Execute(DB);

			if (dt.Rows.Count == 0)
				return false;

			if (UserID > 0)
			{

				foreach(DataRow row in dt.Rows)
				{
					if (HashHelper.Verify(currentPassword, row["Password"].ToDefString()))
					{
						passwordMatch = true;
					}
				}
			}

			return passwordMatch;
		}

		public bool Disconnect(bool clearSessions = false)
		{
			return BaseApp.BaseUserManager.DisconnectUser(UserID, clearSessions);
		}

		public void Release()
		{
			BaseApp.ContainerManager.Release(this);
		}

		#endregion " Public Methods "
	}
}