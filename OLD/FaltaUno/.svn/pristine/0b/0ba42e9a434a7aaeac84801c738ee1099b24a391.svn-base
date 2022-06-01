using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Infrastructure.Interfaces.Repositories
{
	public interface ITeamRepository : ISingleton
	{
		DataTable Get(long id);
		long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
	}
}