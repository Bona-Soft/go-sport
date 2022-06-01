using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Globalization;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IAppSession : IPerSession
	{
		CultureInfo CultureInfo { get; }
		bool IsLogged { get; }
		IBaseUser User { get; }
		string SessionString { get; }
		long LoginID { get; }
		string Language { get; }

		bool Login(string username, string password, bool rememberUser = false);

		bool Login(string username, string password, string ip, bool rememberUser = false);

		bool Login(string username, string password, string ip, string userAgent, bool rememberUser = false);

		bool Login(string username, string password, bool unique, bool rememberUser);

		bool Login(string username, string password, string ip, string userAgent, bool unique, bool rememberUser, short sequence = 1);

		bool Login(string username, string[] password, string ip, string userAgent, bool unique, bool rememberUser);

		bool Logout(bool allSession = false);

		bool ValidateUserName(string username);
	}
}