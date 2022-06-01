using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces.DataBase
{
	public interface IDBDataTableMethods
	{
		DataTable Add(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataTable Delete(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataTable Update(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataTable Get(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataTable Emun(string sTable, OleDbTransaction Trx = null);

		DataTable GetOne(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataTable GetAll(string sTable, OleDbTransaction Trx = null);

		DataTable Execute(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataTable Execute(string sSqlCmd, OleDbTransaction Trx = null);
	}
}