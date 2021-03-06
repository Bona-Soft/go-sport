using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.SPManagers;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Domain.WarehouseStoredProcedure
{
	public class UserSPManager : StoredProceduresManager, IUserSPManager
	{
		public IStoredProcedure<long> AddUserLogin(long userID, string username, string password, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt),
				DB.CreateParameter("Username", username, OleDbType.VarChar),
				DB.CreateParameter("Password", password, OleDbType.VarChar)
			};
			return DB.StoredProcedure<long>("AddUserLogin", dbParams, Trx);
		}

		public IStoredProcedure<long> AddUser(string username, string password, OleDbTransaction Trx = null)
			=> AddUser(0, username, password, null, null, null, Trx);

		public IStoredProcedure<long> AddUser(string username, string password, string name, string lastName, OleDbTransaction Trx = null)
			=> AddUser(0, username, password, name, lastName, null, Trx);

		public IStoredProcedure<long> AddUser(int implementationID, string username, string password, string name, string lastName, OleDbTransaction Trx = null)
			=> AddUser(implementationID, username, password, name, lastName, null, Trx);

		public IStoredProcedure<long> AddUser(string emailAddress, string password, string name, string lastName, string verificationCode, OleDbTransaction Trx = null)
			=> AddUser(0, emailAddress, password, name, lastName, verificationCode, Trx);

		public IStoredProcedure<long> AddUser(int implementationID, string emailAddress, string password, string name, string lastName, string verificationCode, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", emailAddress, OleDbType.VarChar),
				DB.CreateParameter("Password", password, OleDbType.VarChar),
				DB.CreateParameter("Name", name, OleDbType.VarChar),
				DB.CreateParameter("LastName", lastName, OleDbType.VarChar),
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer),
				DB.CreateParameter("VerificationCode", verificationCode, OleDbType.VarChar)
			};
			return DB.StoredProcedure<long>("AddUser", dbParams, Trx);
		}

		public IStoredProcedure<DataSet> EnumUserLogins(long userID, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt)
			};
			return DB.StoredProcedure<DataSet>("EnumUserLogins", dbParams, Trx);
		}

		public IStoredProcedure<DataSet> EnumUserSessions(long userID, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt)
			};
			return DB.StoredProcedure<DataSet>("EnumUserSessions", dbParams, Trx);
		}

		public IStoredProcedure<bool> RemoveUser(long userID, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt)
			};
			return DB.StoredProcedure<bool>("RemoveUser", dbParams, Trx);
		}

		public IStoredProcedure<bool> DeleteUser(long userID, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt)
			};
			return DB.StoredProcedure<bool>("DeleteUser", dbParams, Trx);
		}

		public IStoredProcedure<bool> DisconnectUser(long userID, bool clearSession, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt),
				DB.CreateParameter("ClearSession", clearSession, OleDbType.Boolean)
			};
			return DB.StoredProcedure<bool>("DisconnectUser", dbParams, Trx);
		}

		public IStoredProcedure<bool> RemoveSession(string session, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Session", session, OleDbType.VarChar)
			};
			return DB.StoredProcedure<bool>("RemoveSession", dbParams, Trx);
		}

		public IStoredProcedure<bool> RemoveAllUserSession(long userID, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt)
			};
			return DB.StoredProcedure<bool>("RemoveAllUserSession", dbParams, Trx);
		}

		public IStoredProcedure<int> ExistsUsername(string username, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", username, OleDbType.VarChar)
			};
			return DB.StoredProcedure<int>("ExistsUsername", dbParams, Trx);
		}

		public IStoredProcedure<int> ExistsUsername(int implementationID, string username, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", username, OleDbType.VarChar),
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer)
			};
			return DB.StoredProcedure<int>("ExistsUsername", dbParams, Trx);
		}

		public IStoredProcedure<bool> DeleteLogin(string username, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", username, OleDbType.VarChar)
			};
			return DB.StoredProcedure<bool>("DeleteLogin", dbParams, Trx);
		}

		public IStoredProcedure<bool> DeleteLogin(int implementationID, string username, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", username, OleDbType.VarChar),
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer)
			};
			return DB.StoredProcedure<bool>("DeleteLogin", dbParams, Trx);
		}

		public IStoredProcedure<short> VerifyUserEmail(string emailAddress, string verificationCode, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", emailAddress, OleDbType.VarChar),
				DB.CreateParameter("VerificationCode", verificationCode, OleDbType.VarChar)
			};
			return DB.StoredProcedure<short>("VerifyUserEmail", dbParams, Trx);
		}

		public IStoredProcedure<short> VerifyUserEmail(int implementationID, string emailAddress, string verificationCode, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", emailAddress, OleDbType.VarChar),
				DB.CreateParameter("VerificationCode", verificationCode, OleDbType.VarChar),
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer)
			};
			return DB.StoredProcedure<short>("VerifyUserEmail", dbParams, Trx);
		}

		public IStoredProcedure<string> GetUserVerificationCode(string emailAddress, OleDbTransaction Trx = null)
			=> GetUserVerificationCode( 0, emailAddress, Trx);
		public IStoredProcedure<string> GetUserVerificationCode(int implementationID, string emailAddress, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Username", emailAddress, OleDbType.VarChar),
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer)
			};
			return DB.StoredProcedure<string>("GetUserVerificationCode", dbParams, Trx);
		}
	}
}