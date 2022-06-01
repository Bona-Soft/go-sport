using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Model.Repositories
{
	public class SportRepository : ISportRepository
	{
		public DataTable Get(short sportID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("@SportID", sportID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetSport", dbParams.ToArray());
		}

		public DataTable GetAll()
		{
			return BaseApp.DB.Execute<DataTable>("GetSports");

		}
	}
}