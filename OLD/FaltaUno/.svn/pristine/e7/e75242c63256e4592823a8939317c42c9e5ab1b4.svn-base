using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Infrastructure.Interfaces.Repositories
{
	public interface IHeadquarterRepository : ISingleton
	{
		int Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);

		int Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);

		DataTable Get(int id);

		int Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);

		DataTable SearchHeadquarters(short? sportID, string searchText, float lat, float lng);
	}
}