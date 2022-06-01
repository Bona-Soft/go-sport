using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Collections.Generic;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IBaseUser : IPerUser, IPerImplementation, IPerHost
	{
		long UserID { get; }
		List<ILogins> Logins { get; }
		List<string> Sessions { get; }
		List<IAppSession> ActiveSessions { get; }
		string DefaultLanguage { get; set; }
		int News { get; set; }
		Dictionary<string, int> HasNewsCollection { get; set; }
		long AddLogin(string username, string password);

		long AddLogin(string username, string[] password);

		bool ChangePassword(string newPassword);

		bool ChangePassword(string loginName, string newPassowrd);
		bool CheckPassword(string loginName, string currentPassword);
		bool CheckPassword(string currentPassword);

		bool Disconnect(bool clearSessions = false);

		void Release();
	}
}