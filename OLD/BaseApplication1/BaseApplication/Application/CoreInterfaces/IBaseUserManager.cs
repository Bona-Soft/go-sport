using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Collections.Generic;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IBaseUserManager : ISingleton
	{
		long CreateUser(string username, string password);

		List<IBaseUser> GetAllActiveUsers();

		bool RemoveUser(long UserID);

		bool DeleteUser(long userID);

		bool DisconnectUser(long userID, bool clearSessions = false);

		bool DeleteLogin(string username);

		string GetUserVerificationCode(string emailAddress);
		long RegisterUserPendingVerify(string emailAddress, string password, string name, string lastName, out string verificationCode);
		long RegisterUserPendingVerify(string emailAddress, string password, out string verificationCode);

		short VerifyUserEmail(string emailAddress, string verificationCode);
	}
}