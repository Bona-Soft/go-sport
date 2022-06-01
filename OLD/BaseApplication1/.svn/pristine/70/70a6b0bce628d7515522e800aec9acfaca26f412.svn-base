using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Infrastructure.DB.WarehouseStoredProcedures;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Infrastructure.DB
{
   public class WSP : IWSP
   {
      //private class AuthenticateLogin : spAuthenticateLogin { }
      //public delegate IStoredProcedure AuthenticateLogin(string LoginName, string Password, string Session, string IP, bool Unique, OleDbTransaction Trx = null);
      //public AuthenticateLogin AuthenticateLogin;

     
      public IStoredProcedure<long> AuthenticateLogin(string LoginName, string Password, string Session, string IP, bool Unique, OleDbTransaction Trx = null)
      {
         return spAuthenticateLogin.Get(LoginName, Password, Session, IP, Unique, Trx);

      
      }

      public WSP()
      {

      }

   }
}
