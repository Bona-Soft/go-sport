using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Model.Repositories
{
	public class UserRepository : IUserRepository
	{
		public long UpdatePrivacy(long userPrivacyID, long userID, char value, OleDbTransaction trx = null)
		{
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(BaseApp.DB.CreateParameter("UserPrivacyID", userPrivacyID, OleDbType.BigInt));
         dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));
         dbParams.Add(BaseApp.DB.CreateParameter("Value", value, OleDbType.Char));

         return BaseApp.DB.Execute<long>("UpdateUserPrivacy", dbParams.ToArray(), trx);
		}

      public long AddPrivacy( long userID, string entity, string property, char value, OleDbTransaction trx = null)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(BaseApp.DB.CreateParameter("Entity", entity, OleDbType.VarChar));
         dbParams.Add(BaseApp.DB.CreateParameter("Property", property, OleDbType.VarChar));
         dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));
         dbParams.Add(BaseApp.DB.CreateParameter("Value", value, OleDbType.Char));

         return BaseApp.DB.Execute<long>("AddUserPrivacy", dbParams.ToArray(), trx);
      }

      public DataTable GetPrivacy(long id)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("UserID", id, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetUserPrivacy", dbParams.ToArray());
		}
      public DataTable GetDefaultPrivacy()
		{
			return BaseApp.DB.Execute<DataTable>("GetDefaultUserPrivacy");
		}

		public DataTable Get(long userId)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("UserID", userId, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetUser", dbParams.ToArray());
		}

		public DataTable GetByEmail(string email)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("Email", email, OleDbType.VarChar));

			return BaseApp.DB.Execute<DataTable>("GetUserByEmail", dbParams.ToArray());
		}

		public long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<long>("User", dbParams.ToArray(), trx);
		}

		public long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("User", dbParams.ToArray(), trx);
		}

		public long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			throw new NotImplementedException();
		}

		public bool UpdateAvatar(long userID, string fileName, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));
			dbParams.Add(BaseApp.DB.CreateParameter("Avatar", fileName, OleDbType.VarChar));

			return BaseApp.DB.Execute<bool>("UpdateAvatar", dbParams.ToArray());
		}


	}
}