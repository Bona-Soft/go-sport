using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces.SPManagers
{
	public interface ISessionSPManager : IStoredProceduresManager
	{
		/// <summary>
		///   Verify if the session string exists in the database and is able to login throw it.
		/// </summary>
		/// <param name="session"> Session string to be verify </param>
		/// <param name="ip"> Optional client IP address to check if its bloqued </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> AuthenticateSession(string session, string ip = null, string userAgent = null, OleDbTransaction Trx = null);

		/// <summary>
		///   Retireve the encrypted password of a user login
		/// </summary>
		/// <param name="username"> The user login name who want to retrieve password </param>
		/// <param name="ip"> Optional client IP address to check if its bloqued </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<DataTable> GetLoginPasswords(string username, string ip = null, OleDbTransaction Trx = null);

		/// <summary>
		///   Change all password to a current user or an specific login password by name
		/// </summary>
		/// <param name="userID"> The user ID who want to change password </param>
		/// <param name="password"> The new password to change</param>
		/// <param name="loginName"> If it is not null it will change only the matching login name</param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> ChangePassword(long userID, string password, string loginName = null, OleDbTransaction Trx = null);

		/// <summary>
		///   Retireve all the encrypted password of a user by UserID 
		/// </summary>
		/// <param name="userid"> The user ID who want to retrieve password </param>
		/// <param name="ip"> Optional client IP address to check if its bloqued </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<DataTable> GetLoginPasswordsByUserID(long userid, string ip = null, OleDbTransaction Trx = null);

		/// <summary>
		///   Retireve the encrypted password of a user login at specific implementation.
		/// </summary>
		/// <param name="implementationID"> The implementation of the username that want to search </param>
		/// <param name="username"> The user login name who want to retrieve password </param>
		/// <param name="ip"> Optional client IP address to check if its bloqued </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<DataTable> GetLoginPasswords(int implementationID, string username, string ip = null, OleDbTransaction Trx = null);

		/// <summary>
		///   Logout a single user by login id and session string
		/// </summary>
		/// <param name="LoginID"> The login id where the session must be removed in order to logout </param>
		/// <param name="session"> Session string to be deleted </param>
		/// <param name="allSessions"> Default false, if true it will delete all session of this login </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> Logout(long loginID, string session, bool allSessions = false, OleDbTransaction Trx = null);

		/// <summary>
		///   Logout a single user by session string
		/// </summary>
		/// <param name="session"> Session string to be deleted </param>
		/// <param name="allSessions"> Default false, if true it will delete all session of this login </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<bool> Logout(string session, bool allSessions = false, OleDbTransaction Trx = null);

		/// <summary>
		///   Set a session string to an specific login id
		/// </summary>
		/// <param name="loginID"> Login id correspond to the new session </param>
		/// <param name="session"> Session string to be setted </param>
		/// <param name="ip"> The client IP address where the request come </param>
		/// <param name="unique"> Default false, if true all others session will be cleared before set the new one </param>
		/// <param name="Trx"> The optional transaction where the SP must be executed. If is null the DB execute with his own trx policy </param>
		/// <returns> A stored procedure to be execute using .Execute() with the Data Base Interface as parameter. The datatype IStoredProcedure<T> represent what type of data return after being execute </returns>
		IStoredProcedure<long> SetSession(long loginID, string session, string ip = null, string userAgent = null, bool unique = false, OleDbTransaction Trx = null);
	}
}