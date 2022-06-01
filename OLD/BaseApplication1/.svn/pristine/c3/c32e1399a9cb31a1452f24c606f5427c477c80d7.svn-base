using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.SPManagers;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Domain.WarehouseStoredProcedure
{
	public class SessionSPManager : StoredProceduresManager, ISessionSPManager
	{
		public IStoredProcedure<long> AuthenticateSession(string session, string ip = null, string userAgent = null, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Session", session, OleDbType.VarChar),
				DB.CreateParameter("IP", ip, OleDbType.VarChar),
				DB.CreateParameter("UserAgent", userAgent, OleDbType.VarChar)
			};
			return DB.StoredProcedure<long>("AuthenticateSession", dbParams, Trx);
		}

		public IStoredProcedure<DataTable> GetLoginPasswords(string username, string ip = null, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("LoginName", username, OleDbType.VarChar),
				DB.CreateParameter("IP", ip, OleDbType.VarChar)
			};
			return DB.StoredProcedure<DataTable>("GetLoginPasswords", dbParams, Trx);
		}

		public IStoredProcedure<DataTable> GetLoginPasswordsByUserID(long userid, string ip = null, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userid, OleDbType.BigInt),
				DB.CreateParameter("IP", ip, OleDbType.VarChar)
			};
			return DB.StoredProcedure<DataTable>("GetLoginPasswordsByUserID", dbParams, Trx);
		}


		public IStoredProcedure<DataTable> GetLoginPasswords(int implementationID, string username, string ip = null, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("LoginName", username, OleDbType.VarChar),
				DB.CreateParameter("IP", ip, OleDbType.VarChar),
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer)
			};
			return DB.StoredProcedure<DataTable>("GetLoginPasswords", dbParams, Trx);
		}

		public IStoredProcedure<bool> Logout(long loginID, string session, bool allSessions = false, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Session", session, OleDbType.VarChar),
				DB.CreateParameter("LoginID", loginID, OleDbType.BigInt),
				DB.CreateParameter("AllSessions", allSessions, OleDbType.Boolean)
			};
			return DB.StoredProcedure<bool>("Logout", dbParams, Trx);
		}

		public IStoredProcedure<long> ChangePassword(long userID, string password, string loginName = null, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("UserID", userID, OleDbType.BigInt),
				DB.CreateParameter("Password", password, OleDbType.VarChar),
				DB.CreateParameter("LoginName", loginName, OleDbType.VarChar)
			};
			return DB.StoredProcedure<long>("ChangePassword", dbParams, Trx);
		}

		public IStoredProcedure<bool> Logout(string session, bool allSessions = false, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("Session", session, OleDbType.VarChar),
				DB.CreateParameter("AllSessions", allSessions, OleDbType.Boolean)
			};
			return DB.StoredProcedure<bool>("Logout", dbParams, Trx);
		}

		public IStoredProcedure<long> SetSession(long loginID, string session, string ip = null, string userAgent = null, bool unique = false, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("LoginID", loginID, OleDbType.BigInt),
				DB.CreateParameter("Session", session, OleDbType.VarChar),
				DB.CreateParameter("IP", ip, OleDbType.VarChar),
				DB.CreateParameter("UserAgent", userAgent, OleDbType.VarChar),
				DB.CreateParameter("Unique", unique, OleDbType.Boolean)
			};
			return DB.StoredProcedure<long>("SetSession", dbParams, Trx);
		}
	}
}