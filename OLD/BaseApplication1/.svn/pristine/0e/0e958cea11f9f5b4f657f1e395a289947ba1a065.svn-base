using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Domain.WarehouseStoredProcedure
{
   public class SessionManager : StoredProceduresManager
   {

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

      public IStoredProcedure<long> AuthenticateSession(string session, string ip, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Session", session, OleDbType.VarChar));
         dbParams.Add(DB.CreateParameter("@IP", ip, OleDbType.VarChar));

         return DB.StoredProcedure<long>("AuthenticateSession", dbParams, Trx);
      }

      public IStoredProcedure<bool> ValidateUserName(string username, OleDbTransaction Trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(DB.CreateParameter("@Username", username, OleDbType.VarChar));

         return DB.StoredProcedure<bool>("ValidateUserName", dbParams, Trx);
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
   }
}
