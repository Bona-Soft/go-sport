using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Infrastructure.DB.Methods
{
	public class DataTableMethods : DataBaseMethods, IDBDataTableMethods
	{
		public DataTableMethods(DB parentDataBase)
		{
			ParentDataBase = parentDataBase;
		}

		#region " Private Methods "

		private DataTable ExecuteCmdDT(IStoredProcedure<DataTable> sp)
		{
			return ExecuteCmdDT(sp.Name, sp.Params.ToArray(),sp.Trx);
		}

		private DataTable ExecuteCmdDT(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			OleDbConnection dbConnection;
			OleDbCommand dbCommand;
			OleDbDataAdapter dbAdapter;
			DataSet dsResult;
			bool CloseTrx = false;

			dbCommand = new OleDbCommand();
			dbAdapter = new OleDbDataAdapter();
			dsResult = new DataSet();

         dbConnection = ResolveConnection();

         dbCommand.Connection = dbConnection;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = sSqlCmd;
			if (CommandTimeOut > 0)
			{
				dbCommand.CommandTimeout = CommandTimeOut;
			}

			if (!ParentDataBase.spUseDerivedParameters)
			{
				for (short iCount = 0; iCount < dbParams.Length; iCount++)
				{
					dbCommand.Parameters.Add(dbParams[iCount]);
				}
			}
			else
			{
				ParametersProvider.FillParameters(dbCommand, dbParams);
			}

			if (Trx != null)
			{
				ParentDataBase.SetTransaction(Trx);
			}
			else if (Transaction != null)
			{
				Trx = Transaction;
			}
			else
			{
				CloseTrx = true;
				Trx = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
			}

			try
			{
				dbCommand.Transaction = Trx;
				dbAdapter.SelectCommand = dbCommand;
				dbAdapter.Fill(dsResult);
				if (CloseTrx)
				{
					Trx.Commit();
					dbConnection.Close();
					dbConnection.Dispose();
				}


			}
			catch (Exception ex)
			{
				try
				{
					if (CloseTrx)
					{
						Trx.Rollback();
						dbConnection.Close();
						dbConnection.Dispose();
					}

				}
				catch
				{
					try
					{
						dbConnection.Close();
						dbConnection.Dispose();
					}
					catch { }
				}

				throw ex;
			}

			return dsResult.Tables[0];
		}

		private DataTable ExecuteQueryDT(IStoredProcedure<DataTable> sp)
		{
			return ExecuteQueryDT(sp.Name,sp.Trx);
		}

		private DataTable ExecuteQueryDT(string sSqlCmd, OleDbTransaction Trx = null)
		{
			OleDbConnection dbConnection;
			OleDbCommand dbCommand;
			OleDbDataAdapter dbAdapter;
			DataTable dsResult;
			bool CloseTrx = false;

			dbCommand = new OleDbCommand();
			dbAdapter = new OleDbDataAdapter();
			dsResult = new DataTable();

         dbConnection = ResolveConnection();

         dbCommand.Connection = dbConnection;
			dbCommand.CommandType = CommandType.Text;
			dbCommand.CommandText = sSqlCmd;
			if (CommandTimeOut > 0)
			{
				dbCommand.CommandTimeout = CommandTimeOut;
			}

			if (Transaction == null && Trx == null)
			{
				CloseTrx = true;
				Trx = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
			}
			else if (Transaction != null && Trx == null)
			{
				Trx = Transaction;
			}
			try
			{
				dbCommand.Transaction = Trx;
				dbAdapter.SelectCommand = dbCommand;
				dbAdapter.Fill(dsResult);
				if (CloseTrx)
					Trx.Commit();
			}
			catch (Exception ex)
			{
				Trx.Rollback();
				throw ex;
			}
			finally
			{
				if (CloseTrx)
				{
					dbConnection.Close();
					dbConnection.Dispose();
				}
			}

			return dsResult;
		}

		#endregion " Private Methods "

		#region " Public Methods "

		public DataTable Add(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Add" + sTable, dbParams, Trx);
		}

		public DataTable Delete(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Delete" + sTable, dbParams, Trx);
		}

		public DataTable Update(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Update" + sTable, dbParams, Trx);
		}

		public DataTable Get(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Get" + sAction, dbParams, Trx);
		}

		public DataTable Emun(string sTable, OleDbTransaction Trx = null)
		{
			return Execute("Emun" + sTable, Trx);
		}

		public DataTable GetOne(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("GetOne" + sTable, dbParams, Trx);
		}

		public DataTable GetAll(string sTable, OleDbTransaction Trx = null)
		{
			return Execute("GetAll" + sTable, Trx);
		}

		public DataTable Execute(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return ExecuteCmdDT(sSqlCmd, dbParams, Trx);
		}

		public DataTable Execute(string sSqlCmd, OleDbTransaction Trx = null)
		{
			return ExecuteQueryDT(sSqlCmd, Trx);
		}

		#endregion " Public Methods "
	}
}