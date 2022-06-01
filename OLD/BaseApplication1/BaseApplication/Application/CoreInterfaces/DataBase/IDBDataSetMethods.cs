using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces.DataBase
{
	public interface IDBDataSetMethods
	{
		DataSet Add(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataSet Delete(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataSet Update(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataSet Get(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataSet Emun(string sTable, OleDbTransaction Trx = null);

		DataSet GetOne(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataSet GetAll(string sTable, OleDbTransaction Trx = null);

		DataSet Execute(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		DataSet Execute(string sSqlCmd, OleDbTransaction Trx = null);
	}
}