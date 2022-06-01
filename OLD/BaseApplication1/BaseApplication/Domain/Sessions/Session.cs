using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Cryptography;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;

namespace MYB.BaseApplication.Domain.Sessions
{
	public class BaseAppSession : IAppSession
	{
		#region " Shorcut Properies "

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

		#endregion " Shorcut Properies "

		#region " Private Properies "
		public long LoginID { get; private set; }
		private string _SessionString;
		private long _UserID;
		private const string SessionAppName = "BaseAppSession";
		private const string SessionAppNameLanguage = "BaseAppSessionLanguage";
		private string _SelectedLanguage;
		private List<string> _HubConnections;

		#endregion " Private Properies "

		#region " Constructor Methods "

		public BaseAppSession()
		{
			_UserID = 0;
			LoginID = 0;
		}

		public BaseAppSession(string sessionString)
		{
			SetSessionString(sessionString);
		}

		#endregion " Constructor Methods "

		#region " Private Methods "

		private void CreateCookie(string cookieName, string value, DateTime expiration, bool httpOnly, bool secure)
		{
			CreateCookie(cookieName, value, expiration, httpOnly, secure, "/");
		}

		private void CreateCookie(string cookieName, string value, DateTime expiration, bool httpOnly, bool secure, string path)
		{
			var cookie = new HttpCookie(cookieName, value)
			{
				Expires = expiration
			};
			cookie.HttpOnly = httpOnly;
			cookie.Secure = secure;
			cookie.Path = path;
			if (HttpContext.Current != null)
			{
				HttpContext.Current.Response.Cookies.Remove(cookieName);
				HttpContext.Current.Response.Cookies.Add(cookie);
			}
		}

		private void RemoveCookie(string cookieName)
		{
			if (HttpContext.Current != null
				 && HttpContext.Current.Request.Cookies[cookieName] != null)
			{
				HttpContext.Current.Response.Cookies[cookieName].Value = null;
				HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.UtcNow.AddMonths(-20);
			}
		}

		private void SetSessionString(string session)
		{
			_SessionString = session;
			if (HttpContext.Current != null)
				HttpContext.Current.Session[SessionAppName] = session;
		}

		private long GetUserIDBySession(string session, string ip = "", string userAgent = "")
		{
			long userID = WSP.SessionGroup.AuthenticateSession(session, ip, userAgent).Execute(DB);
			if (userID > 0)
				_SessionString = session;
			return userID;
		}

		#endregion " Private Methods "

		#region " Public Properties "

		public TimeZone LocalTimeZone { get; set; }
		public CultureInfo CultureInfo { get; set; }

		public string Language
		{
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.Session[SessionAppNameLanguage] != null)
				{
					_SelectedLanguage = HttpContext.Current.Session[SessionAppNameLanguage].ToString();
				}
				else
				{
					if (HttpContext.Current != null && HttpContext.Current.Request.Cookies[SessionAppNameLanguage] != null)
					{
						_SelectedLanguage = HttpContext.Current.Request.Cookies[SessionAppNameLanguage].Value;
					}
				}
				if (_SelectedLanguage != null)
					return _SelectedLanguage;
				try
				{
					if (BaseApp.BaseUser().DefaultLanguage != null)
						return BaseApp.BaseUser().DefaultLanguage;
				}
				catch { }
				try
				{
					if (BaseApp.DefaultLanguage != null)
					{
						SelectLenguage(BaseApp.DefaultLanguage);
						return BaseApp.DefaultLanguage;
					}
				}
				catch { }
				return "EN";
			}
		}

		private string GetUserHostAddress()
		{
			if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != null && HttpContext.Current.Request.UserHostAddress != "")
			{
				return HttpContext.Current.Request.UserHostAddress;
			}
			return "NoHostAddress";
		}

		private string GetUserAgent()
		{
			if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserAgent != null && HttpContext.Current.Request.UserAgent != "")
			{
				return HttpContext.Current.Request.UserAgent;
			}
			return "NoUserAgent";
		}

		public bool IsLogged
		{
			get
			{
				if (_UserID <= 0 || _SessionString == null)
				{
					_UserID = GetUserIDBySession(SessionString, GetUserHostAddress(), GetUserAgent());
				}
				return _UserID > 0;
			}
		}

		public IBaseUser User
		{
			get
			{
				if (IsLogged)
					return BaseApp.BaseUser(_UserID);
				return null;
			}
		}

		public string SessionString
		{
			get
			{
				if ((_SessionString == null || _SessionString == "") && HttpContext.Current != null)
				{
					if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionAppName] != null)
					{
						_SessionString = HttpContext.Current.Session[SessionAppName].ToDefString();
					}

					if ((_SessionString == null || _SessionString == "") && HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[SessionAppName] != null)
					{
						_SessionString = HttpContext.Current.Request.Cookies[SessionAppName].Value;
					}
				}

				return _SessionString;
			}
		}

		#endregion " Public Properties "

		#region " Public Methods "

		#region public bool Login: Overload

		public bool Login(string username, string password, bool rememberUser = false) => Login(username, password, "", "", false, rememberUser, 1);

		public bool Login(string username, string password, string ip, bool rememberUser = false) => Login(username, password, ip, "", false, rememberUser, 1);

		public bool Login(string username, string password, string ip, string userAgent, bool rememberUser = false) => Login(username, password, ip, userAgent, false, rememberUser, 1);

		public bool Login(string username, string password, bool unique, bool rememberUser) => Login(username, password, "", "", unique, rememberUser, 1);

		#endregion public bool Login: Overload

		public bool Login(string username, string password, string ip, string userAgent, bool unique, bool rememberUser, short sequence = 1)
		{
			string auxSession = GuidGenerator.GetString() + username + DateTime.Now.ToString();

			DataTable dt = WSP.SessionGroup.GetLoginPasswords(username, ip).Execute(DB);

			if (dt.Rows.Count == 0)
				return false;

			long UserID = Convert.ToInt64(dt.Rows[0]["UserID"]);

			if (UserID > 0)
			{
				if (!HashHelper.Verify(password, dt.Rows[sequence - 1]["Password"].ToString()))
					return false;
				LoginID = Convert.ToInt64(dt.Rows[0]["LoginID"]);
				_UserID = UserID;

				WSP.SessionGroup.SetSession(LoginID, auxSession, ip, userAgent, unique).Execute(DB);
				if (rememberUser)
					CreateCookie(SessionAppName, auxSession, DateTime.UtcNow.AddDays(BaseApp.GeneralParameters.Get<int>("CookieExpireDay", 360)), false, false);

				SetSessionString(auxSession);
			}

			return UserID > 0;
		}

		public bool Login(string username, string[] password, string ip, string userAgent, bool unique, bool rememberUser)
		{
			string auxSession = GuidGenerator.GetString() + username + DateTime.Now.ToString();

			DataTable dt = WSP.SessionGroup.GetLoginPasswords(username, ip).Execute(DB);
			long UserID = Convert.ToInt64(dt.Rows[0]["UserID"]);

			if (UserID > 0 && dt.Rows.Count == password.Length)
			{
				for (int i = 0; i < password.Length; i++)
				{
					if (!HashHelper.Verify(password[i], dt.Rows[i]["Password"].ToString()))
						return false;
				}
				LoginID = Convert.ToInt64(dt.Rows[0]["LoginID"]);
				_UserID = UserID;

				WSP.SessionGroup.SetSession(LoginID, auxSession, ip, userAgent, unique).Execute(DB);
				if (rememberUser)
					CreateCookie(SessionAppName, auxSession, DateTime.UtcNow.AddDays(BaseApp.GeneralParameters.Get<int>("CookieExpireDay", 360)), false, false);
				SetSessionString(auxSession);
			}
			else
			{
				return false;
			}

			return true;
		}

		public bool ValidateUserName(string username)
		{
			return WSP.UserGroup.ExistsUsername(username).Execute(DB) == 1;
		}

		public bool Logout(bool allSessions = false)
		{
			RemoveCookie(SessionAppName);
			RemoveCookie(SessionAppNameLanguage);

			if (HttpContext.Current != null)
			{
				HttpContext.Current.Session.Abandon();
				HttpContext.Current.Session.Remove(SessionAppName);
			}

			if (!allSessions)
				WSP.SessionGroup.Logout(LoginID, SessionString, allSessions).Execute(DB);
			else
				BaseApp.BaseUser(_UserID).Disconnect(true);

			_UserID = 0;
			_SessionString = null;
			LoginID = 0;
			
			return true;
		}

		public void SelectLenguage(string language)
		{
			_SelectedLanguage = language;
			if(HttpContext.Current != null)
				HttpContext.Current.Session[SessionAppNameLanguage] = language;
			CreateCookie(SessionAppNameLanguage, language, DateTime.UtcNow.AddDays(BaseApp.GeneralParameters.Get<int>("CookieExpireDay", 360)), false, false);
		}

		#endregion " Public Methods "
	}
}