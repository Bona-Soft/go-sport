using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces.SPManagers
{
   public interface IUserManagerSP
   {
      IStoredProcedure<long> AddUserLogin(long userID, string username, string password, OleDbTransaction Trx = null);
      IStoredProcedure<long> AddUser(string username, string password, OleDbTransaction Trx = null);
      IStoredProcedure<DataSet> EnumUserLogins(long userID, OleDbTransaction Trx = null);
      IStoredProcedure<long> AddUserLogin(string username, string password, OleDbTransaction Trx = null);
      IStoredProcedure<DataSet> EnumUserSessions(long userID, OleDbTransaction Trx = null);
      IStoredProcedure<bool> RemoveUser(long userID, OleDbTransaction Trx = null);
      IStoredProcedure<bool> DeleteUser(long userID, OleDbTransaction Trx = null);
      IStoredProcedure<bool> DisconnectUser(long userID, bool clearSession, OleDbTransaction Trx = null);
      IStoredProcedure<bool> RemoveSession(string session, OleDbTransaction Trx = null);
   }
}
