using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Model.Repositories
{
	public class TeamRepository : ITeamRepository
	{
		//TODO: Crear BaseRepositry<Team> : IBaseRepositry<ITeam> en el base para 
		//meter estandar y por reflecction el Add, Delete, Get, Update

		public long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("Team", dbParams.ToArray(), trx);
		}

		public long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			throw new NotImplementedException();
		}

		public DataTable Get(long id)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("TeamID", id, OleDbType.BigInt));

			return BaseApp.DB.Get<DataTable>("Team", dbParams.ToArray());
		}

		public long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<long>("Team", dbParams.ToArray(), trx);
		}

	}
}