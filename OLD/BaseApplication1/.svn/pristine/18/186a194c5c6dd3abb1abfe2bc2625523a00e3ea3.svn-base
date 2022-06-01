using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Infrastructure.DB.Methods
{
	public class DataReaderMethods : DataBaseMethods, IDBDataReaderMethods
	{
		public DataReaderMethods(DB parentDataBase)
		{
			ParentDataBase = parentDataBase;
		}

		#region " Private Methods "

		private OleDbDataReader ExecuteCmdDR(IStoredProcedure<OleDbDataReader> sp)
		{
			return ExecuteCmdDR(sp.Name, sp.Params.ToArray());
		}

		private OleDbDataReader ExecuteCmdDR(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			OleDbConnection dbConnection;
			OleDbCommand dbCommand;
			OleDbDataReader dbReader;
			bool CloseTrx = false;

         dbConnection = ResolveConnection();

         dbCommand = dbConnection.CreateCommand();
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
				if (ConnectionData.CloseConnection)
				{
					dbReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
				}
				else
				{
					dbReader = dbCommand.ExecuteReader();
				}
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

			return dbReader;
		}

		private OleDbDataReader ExecuteQueryDR(IStoredProcedure<OleDbDataReader> sp)
		{
			return ExecuteQueryDR(sp.Name);
		}

		private OleDbDataReader ExecuteQueryDR(string sSqlCmd, OleDbTransaction Trx = null)
		{
			OleDbConnection dbConnection;
			OleDbCommand dbCommand;
			OleDbDataReader dbReader;
			bool CloseTrx = false;

         dbConnection = ResolveConnection();

			dbCommand = dbConnection.CreateCommand();
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
				if (ConnectionData.CloseConnection)
				{
					dbReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
				}
				else
				{
					dbReader = dbCommand.ExecuteReader();
				}
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
			return dbReader;
		}

		#endregion " Private Methods "

		#region " Public Methods "

		public OleDbDataReader Add(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Add" + sTable, dbParams, Trx);
		}

		public OleDbDataReader Delete(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Delete" + sTable, dbParams, Trx);
		}

		public OleDbDataReader Update(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Update" + sTable, dbParams, Trx);
		}

		public OleDbDataReader Get(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Get" + sAction, dbParams, Trx);
		}

		public OleDbDataReader Emun(string sTable, OleDbTransaction Trx = null)
		{
			return Execute("Emun" + sTable, Trx);
		}

		public OleDbDataReader GetOne(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("GetOne" + sTable, dbParams, Trx);
		}

		public OleDbDataReader GetAll(string sTable, OleDbTransaction Trx = null)
		{
			return Execute("GetAll" + sTable, Trx);
		}

		public OleDbDataReader Execute(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return ExecuteCmdDR(sSqlCmd, dbParams, Trx);
		}

		public OleDbDataReader Execute(string sSqlCmd, OleDbTransaction Trx = null)
		{
			return ExecuteQueryDR(sSqlCmd, Trx);
		}

		#endregion " Public Methods "
	}
}