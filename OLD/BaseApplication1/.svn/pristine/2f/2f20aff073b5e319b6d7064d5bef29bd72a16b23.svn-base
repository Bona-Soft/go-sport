using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace MYB.BaseApplication.Domain.WarehouseStoredProcedure
{
   public class UserManager : StoredProceduresManager
   {
      
      public IStoredProcedure<long> AddUserLogin(long userID, string username, string password, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@UserID", userID, OleDbType.BigInt));
         dbParams.Add(DB.CreateParameter("@Username", username, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Password", password, OleDbType.VarChar));

         return DB.StoredProcedure<long>("AddUserLogin", dbParams, Trx);
      }

      public IStoredProcedure<long> AddUserLogin(string username, string password, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Username", username, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Password", password, OleDbType.VarChar));

         return DB.StoredProcedure<long>("AddUser", dbParams, Trx);
      }

      public IStoredProcedure<long> AddUser(string username, string password, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Username", username, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@Password", password, OleDbType.VarChar));

         return DB.StoredProcedure<long>("AddUserLogin", dbParams, Trx);
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
