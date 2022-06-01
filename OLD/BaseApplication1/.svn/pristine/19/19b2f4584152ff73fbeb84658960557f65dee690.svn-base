using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces.DataBase
{
	public interface IDBDataReaderMethods
	{
		OleDbDataReader Add(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		OleDbDataReader Delete(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		OleDbDataReader Update(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		OleDbDataReader Get(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		OleDbDataReader Emun(string sTable, OleDbTransaction Trx = null);

		OleDbDataReader GetOne(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		OleDbDataReader GetAll(string sTable, OleDbTransaction Trx = null);

		OleDbDataReader Execute(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

		OleDbDataReader Execute(string sSqlCmd, OleDbTransaction Trx = null);
	}
}