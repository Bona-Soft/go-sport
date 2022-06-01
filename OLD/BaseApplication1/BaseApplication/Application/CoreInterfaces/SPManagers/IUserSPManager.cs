using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces.SPManagers
{
	public interface IUserSPManager : IStoredProceduresManager
	{
		/// <summary>
		///   Add a new login to a single user
		/// </summary>
		/// <param name="userID"> UserID who the new login must be added </param>
		/// <param name="username"> The new username string max 255 digits </param>
		/// <param name="password"> The password of the new login </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> AddUserLogin(long userID, string username, string password, OleDbTransaction Trx = null);

		/// <summary>
		///   Add a new user to the system
		/// </summary>
		/// <param name="username"> The new username string max 255 digits </param>
		/// <param name="password"> The password of the new login </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> AddUser(string username, string password, string name, string lastName, OleDbTransaction Trx = null);

		/// <summary>
		///   Add a new user to the system
		/// </summary>
		/// <param name="username"> The new username string max 255 digits </param>
		/// <param name="password"> The password of the new login </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> AddUser(string username, string password, OleDbTransaction Trx = null);

		/// <summary>
		///   Add a new user to the implementation.
		/// </summary>
		/// <param name="implementationID"> The implementation id where the new user will be created </param>
		/// <param name="username"> The new username string max 255 digits of the new user </param>
		/// <param name="password"> The password of the new login </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> AddUser(int implementationID, string username, string password, string name, string lastName, OleDbTransaction Trx = null);

		/// <summary>
		///   Add a new user to the system with an email as username and locked until the SP VerifyUserEmail will be executed
		/// </summary>
		/// <param name="emailAddress"> The new username string max 255 digits </param>
		/// <param name="password"> The password of the new login </param>
		/// <param name="verificationCode"> The verification code to be verify in sp VerifyUserEmail</param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> AddUser(string emailAddress, string password, string name, string lastName, string verificationCode, OleDbTransaction Trx = null);

		/// <summary>
		///   Add a new user to the system with an email as username and locked until the SP VerifyUserEmail will be executed to an especific implementation
		/// </summary>
		/// <param name="implementationID"> The implementation id where the new user will be created </param>
		/// <param name="emailAddress"> The new username string max 255 digits </param>
		/// <param name="password"> The password of the new login </param>
		/// <param name="verificationCode"> The verification code to be verify in sp VerifyUserEmail</param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> AddUser(int implementationID, string emailAddress, string password, string name, string lastName, string verificationCode, OleDbTransaction Trx = null);

		/// <summary>
		///   Get the list of logins that a single user has.
		/// </summary>
		/// <param name="userID"> User who wants to get the list of logins </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<DataSet> EnumUserLogins(long userID, OleDbTransaction Trx = null);

		/// <summary>
		///   Get the list of session that a single user has.
		/// </summary>
		/// <param name="userID"> User who wants to get the list of session </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<DataSet> EnumUserSessions(long userID, OleDbTransaction Trx = null);

		/// <summary>
		///   Logical remove from the database to a single user.
		/// </summary>
		/// <param name="userID"> User id who want to do a logical delete of the system </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> RemoveUser(long userID, OleDbTransaction Trx = null);

		/// <summary>
		///   Delete permanently an user of the system
		/// </summary>
		/// <param name="userID"> User to be deleted </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> DeleteUser(long userID, OleDbTransaction Trx = null);

		/// <summary>
		///   Disconnect a user for the last or all session.
		/// </summary>
		/// <param name="userID"> User id to be disconnected </param>
		/// <param name="clearSession"> If true, all session from this user will be deleted </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> DisconnectUser(long userID, bool clearSession, OleDbTransaction Trx = null);

		/// <summary>
		///   Remove an specific session of the data base.
		/// </summary>
		/// <param name="session"> The session string to be removed </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> RemoveSession(string session, OleDbTransaction Trx = null);

		/// <summary>
		///   Remove all session for a user.
		/// </summary>
		/// <param name="userID"> The user id who all session will be removed </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> RemoveAllUserSession(long userID, OleDbTransaction Trx = null);

		/// <summary>
		///   Return an int that represent if a user exists in the system or the code that why not.
		/// </summary>
		/// <param name="username"> The username to verify if exists in database </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<int> ExistsUsername(string username, OleDbTransaction Trx = null);

		/// <summary>
		///   Return an int that represent if a user exists in the system or the code that why for one implementation.
		/// </summary>
		/// <param name="username"> The username to verify if exists in database by implementation </param>
		/// <param name="implementationID"> The implementation of a user login to verify if exists </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<int> ExistsUsername(int implementationID, string username, OleDbTransaction Trx = null);

		/// <summary>
		///   Permanent delete of a user login.
		/// </summary>
		/// <param name="username"> The username to be deleted </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> DeleteLogin(string username, OleDbTransaction Trx = null);

		/// <summary>
		///   Permanent delete of a user login of an implementation
		/// </summary>
		/// <param name="username"> The username to be deleted </param>
		/// <param name="implementationID"> The implementation of the username to be deleted </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> DeleteLogin(int implementationID, string username, OleDbTransaction Trx = null);

		IStoredProcedure<short> VerifyUserEmail(string emailAddress, string verificationCode, OleDbTransaction Trx = null);

		IStoredProcedure<short> VerifyUserEmail(int implementationID, string emailAddress, string verificationCode, OleDbTransaction Trx = null);

		IStoredProcedure<string> GetUserVerificationCode(string emailAddress, OleDbTransaction Trx = null);
		IStoredProcedure<string> GetUserVerificationCode(int implementationID, string emailAddress, OleDbTransaction Trx = null);
	}
}