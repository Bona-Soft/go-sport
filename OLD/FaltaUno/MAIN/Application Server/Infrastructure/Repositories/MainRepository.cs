using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Infrastructure.Repositories
{
	public class MainRepository : IMainRepository
	{
		public DataTable GetLocations(string searchText, short? sportID, int groupMember, long? userID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();
			dbParams.Add(BaseApp.DB.CreateParameter("SearchText", searchText, OleDbType.VarChar));
			dbParams.Add(BaseApp.DB.CreateParameter("SportID", sportID, OleDbType.TinyInt));
			dbParams.Add(BaseApp.DB.CreateParameter("GroupMemberID", groupMember, OleDbType.Integer));
			dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetLocations", dbParams.ToArray());
		}

      public DataTable GetLocation(long locationID)
      {
         List<OleDbParameter> dbParams;

         dbParams = new List<OleDbParameter>();
         dbParams.Add(BaseApp.DB.CreateParameter("LocationID", locationID, OleDbType.BigInt));

         return BaseApp.DB.Execute<DataTable>("GetLocation", dbParams.ToArray());
      }

      public long AddLocation(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("Location", dbParams.ToArray(), trx);
		}

		public long AddComment(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("Comment", dbParams.ToArray());
		}

		public long UpdateLocation(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<long>("Location", dbParams.ToArray(), trx);
		}
	}
}