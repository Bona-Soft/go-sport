using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces.SPManagers
{
   public interface ISessionManagerSP
   {
      IStoredProcedure<DataSet> AuthenticateLogin(string LoginName, string Password, string Session, string IP, bool Unique, OleDbTransaction Trx = null);
      IStoredProcedure<DataTable> GetLoginPasswords(string username, string ip, OleDbTransaction Trx = null);
      IStoredProcedure<bool> Logout(long loginID, string session, bool allSessions, OleDbTransaction Trx = null);
      IStoredProcedure<bool> ValidateUserName(string username, OleDbTransaction Trx = null);
      IStoredProcedure<long> AuthenticateSession(string session, string ip, OleDbTransaction Trx = null);
      IStoredProcedure<long> SetSession(long loginID, string session, string ip, bool Unique, OleDbTransaction Trx = null);
   }
}
