using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Infrastructure.DB.Methods
{
	public class ObjectMethods : DataBaseMethods, IDBObjectMethods
	{
		public ObjectMethods(DB parentDataBase)
		{
			ParentDataBase = parentDataBase;
		}

		#region " Private Methods "

		private object ExecuteCmd(IStoredProcedure<object> sp)
		{
			return ExecuteCmd(sp.Name, sp.Params.ToArray());
		}

		private object ExecuteCmd(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			object oResult;
			OleDbConnection dbConnection;
			OleDbCommand dbCommand;
			OleDbDataAdapter dbAdapter;
			bool CloseTrx = false;

         dbConnection = ResolveConnection();

         dbAdapter = new OleDbDataAdapter();

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
				int L = 0;

				dbCommand = dbConnection.CreateCommand();
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = sSqlCmd;

				if (CommandTimeOut > 0)
				{
					dbCommand.CommandTimeout = CommandTimeOut;
				}

				if (!ParentDataBase.spUseDerivedParameters)
				{
					dbCommand.Parameters.Add(
						new OleDbParameter("RETURN_VALUE", OleDbType.BigInt)
						{
							Direction = ParameterDirection.ReturnValue
						});

					for (short iCount = 0; iCount < dbParams.Length; iCount++)
					{
						dbCommand.Parameters.Add(dbParams[iCount]);
					}
					L++;
				}
				else
				{
					ParametersProvider.FillParameters(dbCommand, dbParams);

					if (dbCommand.Parameters["RETURN_VALUE"] != null)
						dbCommand.Parameters["RETURN_VALUE"].OleDbType = OleDbType.BigInt;
				}
				dbCommand.Transaction = Trx;
				oResult = dbCommand.ExecuteNonQuery();

				if (dbCommand.Parameters["RETURN_VALUE"] != null)
				{
					oResult = dbCommand.Parameters["RETURN_VALUE"].Value;
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
					if(CloseTrx)
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

			return oResult;
		}

		private object ExecuteQuery(IStoredProcedure<object> sp)
		{
			return ExecuteQuery(sp.Name);
		}

		private object ExecuteQuery(string sSqlCmd, OleDbTransaction Trx = null)
		{
			object oResult;
			OleDbConnection dbConnection;
			OleDbCommand dbCommand;
			OleDbDataAdapter dbAdapter;
			bool CloseTrx = false;

			if (Connection != null)
			{
            if (Connection.State == ConnectionState.Closed)
            {
               Connection = CreateOleDbConnection();
               Connection.Open();
            }
            dbConnection = Connection;
			}
			else
			{
				dbConnection = CreateOleDbConnection();
				dbConnection.Open();
			}

			dbAdapter = new OleDbDataAdapter();

			if (Transaction == null && Trx == null)
			{
				CloseTrx = true;
				Trx = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
			}
			else if (Transaction != null && Trx == null)
			{
				Trx = Transaction;
			}
			else if (Transaction == null && Trx != null)
			{
				ParentDataBase.SetTransaction(Trx);
			}

			try
			{
				dbCommand = dbConnection.CreateCommand();
				dbCommand.CommandType = CommandType.Text;
				dbCommand.CommandText = sSqlCmd;
				if (CommandTimeOut > 0)
				{
					dbCommand.CommandTimeout = CommandTimeOut;
				}

				dbCommand.Transaction = Trx;
				oResult = dbCommand.ExecuteNonQuery();
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
			return oResult;
		}

		#endregion " Private Methods "

		#region " Public Methods "

		public object Add(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Add" + sTable, dbParams, Trx);
		}

		public object Delete(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Delete" + sTable, dbParams, Trx);
		}

		public object Update(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Update" + sTable, dbParams, Trx);
		}

		public object Get(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("Get" + sAction, dbParams, Trx);
		}

		public object Emun(string sTable, OleDbTransaction Trx = null)
		{
			return Execute("Emun" + sTable, Trx);
		}

		public object GetOne(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return Execute("GetOne" + sTable, dbParams, Trx);
		}

		public object GetAll(string sTable, OleDbTransaction Trx = null)
		{
			return Execute("GetAll" + sTable, Trx);
		}

		public object Execute(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
		{
			return ExecuteCmd(sSqlCmd, dbParams, Trx);
		}

		public object Execute(string sSqlCmd, OleDbTransaction Trx = null)
		{
			return ExecuteQuery(sSqlCmd, Trx);
		}

		#endregion " Public Methods "
	}
}