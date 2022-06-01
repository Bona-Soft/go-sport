using MYB.BaseApplication.Framework.Cryptography;
using MYB.BaseApplication.Security.Configuration;
using System;
using System.Web;
using System.Web.Security;

namespace MYB.BaseApplication.Infrastructure.Security
{
	public class AuthenticationStatus
	{
		//The authenticated user has its own session identifier
		//otherwise the user is anonymous
		public string SessionID;

		public string UserIdentifier;
		public DateTime SessionStartDateTime;
		public DateTime ExpirationDateTime;

		//Status = 0, Authenticated
		//Status = 1, Auth cookie not present --- //Status = 1,  ASP.NET Session unavailable
		//Status = 2, Ticket decryption failed
		//Status = 3, Ticket Expired
		//Status = 4, Empty Ticket Name
		//Status = 5, Custom Session not valid

		public int Status;
		public string StatusDetails;
	}

	public class AuthorizationStatus
	{
		public bool Authorized;
	}

	public class Constants
	{
		public static readonly string SESSION_TOKEN_COOKIE_DEFAULT = "ASP.NET_SessionID";
		public static readonly string SSN_USER_SESSIONID = "UserSessionID";
	}

	public class SessionManagement
	{
		private HttpContext _context;
		private SecuritySection _securitySettings;

		#region Website Session Routines

		private void ConfigureSessionStorage()
		{

		}

		private string GenerateSessionID(string userName)
		{
			return Convert.ToBase64String(HMACManagement.CreateSecretKey());
		}

		private string GetDefaultSessionIdentifier()
		{
			var cookieSession = _context.Request.Cookies[Constants.SESSION_TOKEN_COOKIE_DEFAULT];
			return cookieSession == null ? null : cookieSession.Value;
		}

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
			_context.Response.Cookies.Remove(cookieName);
			_context.Response.Cookies.Add(cookie);
		}

		private void RemoveCookie(string cookieName)
		{
			if (_context != null
				 && _context.Request.Cookies[cookieName] != null)
			{
				_context.Response.Cookies[cookieName].Value = null;
				_context.Response.Cookies[cookieName].Expires = DateTime.UtcNow.AddMonths(-20);
			}
		}

		private void RemoveOutdatedSessionEntries()
		{
			//Clear all session variables
			if (!String.IsNullOrEmpty(GetDefaultSessionIdentifier()))
			{
				
			}
		}

		private void RemoveSessionTokenCookies()
		{
			//Invalidates the cookies
			RemoveCookie(Constants.SESSION_TOKEN_COOKIE_DEFAULT);
			if (_context != null) FormsAuthentication.SignOut();
			RemoveCookie(FormsAuthentication.FormsCookieName);
		}

		public SessionManagement(HttpContext context)
		{
			_context = context;
			_securitySettings = SecuritySettings.GetSecuritySection();
			ConfigureSessionStorage();
		}

		public void StartSession(string userIdentifier)
		{
			RemoveOutdatedSessionEntries();
			if (_securitySettings.AuthEnabled)
				GenerateFormsAuthTicketAndCookieTimeouts(userIdentifier);
		}

		public void EndSession()
		{
			RemoveOutdatedSessionEntries();
			RemoveSessionTokenCookies();
		}

		public AuthenticationStatus GetAuthenticationStatus()
		{
			if (_securitySettings.AuthEnabled)
			{
				//Look for the ASP.NET Session in the cookies
				if (String.IsNullOrEmpty(GetDefaultSessionIdentifier()))
				{
					//_context.Response.StatusCode = 412; //Precondition Failed
					//_context.Response.StatusDescription = "Precondition Failed: Session ASP.NET unavailable";
					return new AuthenticationStatus()
					{
						Status = 1,
						StatusDetails = "ASP.NET Session unavailable"
					};
				}

				if (_context.Request.Cookies[_securitySettings.CookieName] == null)
				{
					return new AuthenticationStatus()
					{
						Status = 1,
						StatusDetails = "Authentication cookie not present."
					};
				}

				HttpCookie authCookie = _context.Request.Cookies[_securitySettings.CookieName];

				FormsAuthenticationTicket ticket = null;
				try
				{
					ticket = FormsAuthentication.Decrypt(authCookie.Value);
				}
				catch
				{
					return new AuthenticationStatus()
					{
						Status = 2,
						StatusDetails = "Ticket decryption failed"
					};
				}

				if (ticket.Expiration.ToUniversalTime().CompareTo(DateTime.UtcNow) <= 0)
				{
					return new AuthenticationStatus()
					{
						Status = 3,
						StatusDetails = "Ticket Expired."
					};
				}

				if (String.IsNullOrEmpty(ticket.Name))
				{
					return new AuthenticationStatus()
					{
						Status = 4,
						StatusDetails = "Empty Ticket Name"
					};
				}


				return new AuthenticationStatus()
				{
					Status = 0,
					UserIdentifier = ticket.Name,
					SessionID = ticket.UserData,
					ExpirationDateTime = ticket.Expiration.ToUniversalTime(),
					SessionStartDateTime = ticket.IssueDate.ToUniversalTime()
				};
			}
			else
			{
				return new AuthenticationStatus()
				{
					Status = 0,
					UserIdentifier = String.Empty,
					SessionID = String.Empty,
				};
			}
		}

		private void GenerateFormsAuthTicketAndCookieTimeouts(string userIdentifier)
		{
			//Stores the session identifier in the session storage and the session identifier for the
			//authenticated user
			var newSessionID = GenerateSessionID(userIdentifier);

			// create the new cookie expiration
			DateTime sessionStartDateTime = DateTime.UtcNow;
			DateTime cookieExpiration = sessionStartDateTime.AddMinutes(_securitySettings.SessionExp);

			// define the new authentication ticket
			var ticket =
					  new FormsAuthenticationTicket(
							1,
							userIdentifier,
							sessionStartDateTime,
							cookieExpiration,
							true,
							newSessionID,
							_securitySettings.CookiePath);

			// ticket must be encrypted
			string encryptedTicket = FormsAuthentication.Encrypt(ticket);
			// create cookie to contain encrypted auth ticket
			CreateCookie(_securitySettings.CookieName, encryptedTicket, ticket.Expiration, true, (_securitySettings.CookieProtocol == HttpEnum.https), _securitySettings.CookiePath);
		}

		#endregion Website Session Routines
	}
}