using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces.DataBase
{
	public interface IDBObjectMethods
	{
		object Add(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		object Delete(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		object Update(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		object Get(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		object Emun(string sTable, OleDbTransaction Trx = null);

		object GetOne(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		object GetAll(string sTable, OleDbTransaction Trx = null);

		object Execute(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		object Execute(string sSqlCmd, OleDbTransaction Trx = null);
	}
}