using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Model.Repositories
{
	public class HeadquarterRepository : IHeadquarterRepository
	{
		public int Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<int>("Headquarter", dbParams.ToArray(), trx);
		}

		public int Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			throw new NotImplementedException();
		}

		public DataTable Get(int id)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("@HeadquarterID", id, OleDbType.Integer));

			return BaseApp.DB.Get<DataTable>("Headquarter", dbParams.ToArray());
		}

		public int Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<int>("Headquarter", dbParams.ToArray(), trx);
		}

		public DataTable SearchHeadquarters(short? sportID, string searchText, float lat, float lng)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

            dbParams.Add(BaseApp.DB.CreateParameter("@SportID", lng, OleDbType.TinyInt));
            dbParams.Add(BaseApp.DB.CreateParameter("@SearchText", searchText, OleDbType.VarChar));
			dbParams.Add(BaseApp.DB.CreateParameter("@Lat", lat, OleDbType.Decimal));
			dbParams.Add(BaseApp.DB.CreateParameter("@Lng", lng, OleDbType.Decimal));
			
			return BaseApp.DB.Execute<DataTable>("GetHeadquarters", dbParams.ToArray());
		}
	}
}