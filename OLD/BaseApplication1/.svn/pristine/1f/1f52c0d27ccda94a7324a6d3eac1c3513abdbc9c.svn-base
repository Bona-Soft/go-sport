using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreApplication
{
   public class WSP2 : IWSP2
   {
      private IDB DB
      {
         get
         {
            return BaseApp.DB;
         }
      }

      public WSP2()
      {

      }

      public IStoredProcedure<DataSet> AuthenticateLogin(string LoginName, string Password, string Session, string IP, bool Unique, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@LoginName", LoginName, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Password", Password, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Session", Session, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@IP", IP, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Unique", Unique, OleDbType.Boolean));

         return DB.StoredProcedure<DataSet>("AuthenticateLogin", dbParams, Trx);
      }

      public IStoredProcedure<DataTable> GetLoginPasswords(string username, string ip, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserName", username, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@IP", ip, OleDbType.VarChar));

         return DB.StoredProcedure<DataTable>("GetLoginPasswords", dbParams, Trx);
      }

      public IStoredProcedure<bool> Logout(long loginID, string session, bool allSessions, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@LoginID", loginID, OleDbType.BigInt));
         dbParams.Add(DB.CreateParameter("@Session", session, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@AllSessions", allSessions, OleDbType.Boolean));

         return DB.StoredProcedure<bool>("Logout", dbParams, Trx);
      }

      public IStoredProcedure<bool> ValidateUserName(string username, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Username", username, OleDbType.VarChar));

         return DB.StoredProcedure<bool>("ValidateUserName", dbParams, Trx);
      }

      public IStoredProcedure<long> AuthenticateSession(string session, string ip, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Session", session, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@IP", ip, OleDbType.VarChar));

         return DB.StoredProcedure<long>("AuthenticateSession", dbParams, Trx);
      }

      public IStoredProcedure<long> SetSession(long loginID, string session, string ip, bool unique, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@LoginID", loginID, OleDbType.BigInt));
         dbParams.Add(DB.CreateParameter("@Session", session, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@IP", ip, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Unique", unique, OleDbType.Boolean));

         return DB.StoredProcedure<long>("SetSession", dbParams, Trx);
      }

      public IStoredProcedure<long> AddUserLogin(long userID, string username, string password, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserID", userID, OleDbType.BigInt));
         dbParams.Add(DB.CreateParameter("@Username", username, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Password", password.ToHash(), OleDbType.VarChar));

         return DB.StoredProcedure<long>("AddUserLogin", dbParams, Trx);
      }

      public IStoredProcedure<long> AddUser(string username, string password, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Username", username, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Password", password.ToHash(), OleDbType.VarChar));

         return DB.StoredProcedure<long>("AddUser", dbParams, Trx);
      }

      public IStoredProcedure<DataSet> EnumUserLogins(long userID, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserID", userID, OleDbType.BigInt));

         return DB.StoredProcedure<DataSet>("EnumUserLogins", dbParams, Trx);
      }

      public IStoredProcedure<DataSet> EnumUserSessions(long userID, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserID", userID, OleDbType.BigInt));

         return DB.StoredProcedure<DataSet>("EnumUserSessions", dbParams, Trx);
      }

      public IStoredProcedure<bool> RemoveUser(long userID, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserID", userID, OleDbType.BigInt));

         return DB.StoredProcedure<bool>("RemoveUser", dbParams, Trx);
      }

      public IStoredProcedure<bool> DeleteUser(long userID, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserID", userID, OleDbType.BigInt));

         return DB.StoredProcedure<bool>("DeleteUser", dbParams, Trx);
      }

      public IStoredProcedure<bool> DisconnectUser(long userID, bool clearSession, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserID", userID, OleDbType.BigInt));
         dbParams.Add(DB.CreateParameter("@ClearSession", clearSession, OleDbType.Boolean));

         return DB.StoredProcedure<bool>("DisconnectUser", dbParams, Trx);
      }

      public IStoredProcedure<bool> RemoveSession(string session, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Session", session, OleDbType.VarChar));

         return DB.StoredProcedure<bool>("RemoveSession", dbParams, Trx);
      }
   }
}
