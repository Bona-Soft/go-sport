using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.BaseApplication.Infrastructure.DB.Methods;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace MYB.BaseApplication.Infrastructure.DB
{
   public class DB : IDataBase
   {
      #region " Properties "

      private OleDbConnection _Connection;

      public int CommandTimeOut;
      public OleDbTransaction Transaction { get; set; }

      public OleDbConnection Connection
      {
         get
         {
            return _Connection;
         }
         set
         {
            _Connection = value;
            _Connection.Open();
         }
      }

      public IConnectionData ConnectionData { get; private set; }

      public bool spUseDerivedParameters { get; set; }

      #endregion " Properties "

      #region " Constructor Methods "

      public DB(IConnectionData connectionData)
      {
         OleDbConnection dbConnection;
         this.ConnectionData = connectionData;

         dbConnection = CreateOleDbConnection();
         dbConnection.Dispose();
         CommandTimeOut = 0;
         spUseDerivedParameters = true;

         DataReaderGroup = new DataReaderMethods(this);
         DataSetGroup = new DataSetMethods(this);
         DataTableGroup = new DataTableMethods(this);
         DataObjectGroup = new ObjectMethods(this);
      }

      #endregion " Constructor Methods "

      #region " Group Properties "

      public IDBDataReaderMethods DataReaderGroup { get; private set; }
      public IDBDataSetMethods DataSetGroup { get; private set; }
      public IDBDataTableMethods DataTableGroup { get; private set; }
      public IDBObjectMethods DataObjectGroup { get; private set; }

      #endregion " Group Properties "

      #region " Private SQL Methods "

      private T ExecuteCmd<T>(IStoredProcedure<T> sp)
      {
         return (T)DataObjectGroup.Execute(sp.Name, sp.Params.ToArray());
      }

      private T ExecuteQuery<T>(IStoredProcedure<T> sp)
      {
         return (T)DataObjectGroup.Execute(sp.Name);
      }

      public OleDbConnection CreateOleDbConnection()
      {
         return new OleDbConnection(ConnectionData.ConnectionString);
      }

      #endregion " Private SQL Methods "

      #region " Public SQL Methods "

      #region " Dinamic method returned "

      public T Add<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         return Execute<T>("Add" + sTable, dbParams, Trx);
      }

      public T Delete<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         return Execute<T>("Delete" + sTable, dbParams, Trx);
      }

      public T Update<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         return Execute<T>("Update" + sTable, dbParams, Trx);
      }

      public T Get<T>(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         return Execute<T>("Get" + sAction, dbParams, Trx);
      }

      public T Emun<T>(string sTable, OleDbTransaction Trx = null)
      {
         return Execute<T>("Emun" + sTable, Trx);
      }

      public T GetOne<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         return Execute<T>("GetOne" + sTable, dbParams, Trx);
      }

      public T GetAll<T>(string sTable, OleDbTransaction Trx = null)
      {
         return Execute<T>("GetAll" + sTable, Trx);
      }

      public T Execute<T>(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         if (Trx == null && Transaction != null)
         {
            Trx = Transaction;
         }
			if (typeof(T) == typeof(string))
			{
				DataTable dt = DataTableGroup.Execute(sSqlCmd, dbParams, Trx);
				if (dt.Rows.Count > 0 && dt.Columns.Count > 0)
				{
					return dt.Rows[0].Field<T>(0);
				}
				return default;
			}
			if (typeof(T) == typeof(DataTable)) return (T)Convert.ChangeType(DataTableGroup.Execute(sSqlCmd, dbParams, Trx), typeof(T));
         if (typeof(T) == typeof(OleDbDataReader)) return (T)Convert.ChangeType(DataReaderGroup.Execute(sSqlCmd, dbParams, Trx), typeof(T));
         if (typeof(T) == typeof(DataSet)) return (T)Convert.ChangeType(DataSetGroup.Execute(sSqlCmd, dbParams, Trx), typeof(T));
         if (typeof(T) == typeof(object)) return (T)Convert.ChangeType(DataObjectGroup.Execute(sSqlCmd, dbParams, Trx), typeof(T));
         return (T)Convert.ChangeType(DataObjectGroup.Execute(sSqlCmd, dbParams, Trx), typeof(T));
      }

      public T Execute<T>(string sSqlCmd, OleDbTransaction Trx = null)
      {
         if (Trx == null && Transaction != null)
         {
            Trx = Transaction;
         }
			if (typeof(T) == typeof(string))
			{
				DataTable dt = DataTableGroup.Execute(sSqlCmd, Trx);
				if(dt.Rows.Count > 0  && dt.Columns.Count > 0)
				{
					return dt.Rows[0].Field<T>(0);
				}
				return default;
			}
			if (typeof(T) == typeof(DataTable)) return (T)Convert.ChangeType(DataTableGroup.Execute(sSqlCmd, Trx), typeof(T));
         if (typeof(T) == typeof(OleDbDataReader)) return (T)Convert.ChangeType(DataReaderGroup.Execute(sSqlCmd, Trx), typeof(T));
         if (typeof(T) == typeof(DataSet)) return (T)Convert.ChangeType(DataSetGroup.Execute(sSqlCmd, Trx), typeof(T));
         if (typeof(T) == typeof(object)) return (T)Convert.ChangeType(DataObjectGroup.Execute(sSqlCmd, Trx), typeof(T));
			return (T)Convert.ChangeType(DataObjectGroup.Execute(sSqlCmd, Trx), typeof(T));
      }

      public T Execute<T>(IStoredProcedure<T> SP)
      {
         if (SP.Trx == null && Transaction != null)
         {
            SP.Trx = Transaction;
         }

         if (SP.Params != null)
            return Execute<T>(SP.Name, SP.Params.ToArray(), SP.Trx);
         return Execute<T>(SP.Name, SP.Trx);
      }

      #endregion " Dinamic method returned "

      #region " Escalar method returned "

      public Type ExecuteScalar<Type>(IStoredProcedure<DataSet> sp) where Type : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>
		{
			DataSet result = Execute<DataSet>(sp);
			if(result != null && result.Tables[0].Rows.Count > 0)
				return result.Tables[0].Rows[0].Field<Type>(0);
			return default(Type);
      }

		 public Type ExecuteScalar<Type>(IStoredProcedure<DataTable> sp) where Type : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>
		{
			DataTable result = Execute<DataTable>(sp);
			if(result != null && result.Rows.Count > 0)
				return result.Rows[0].Field<Type>(0);
			return default;
      }

		public Type ExecuteScalar<Type>(string sTable, List<OleDbParameter> dbParams, OleDbTransaction Trx = null) where Type : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>
		{
			StoredProcedure<DataSet> sp = new StoredProcedure<DataSet>(sTable, dbParams, Trx);
			return ExecuteScalar<Type>(sp);
		}

		public Type ExecuteScalar<Type>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null) where Type : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>
		{
         StoredProcedure<DataSet> sp = new StoredProcedure<DataSet>(sTable, dbParams.ToList<OleDbParameter>(), Trx);
         return ExecuteScalar<Type>(sp);
      }


      #endregion " Escalar method returned "

      #region " Columns List Method "

      public List<Type> GetColumnList<Type>(IStoredProcedure<DataSet> sp)
      {
         DataTable dt = Execute<DataSet>(sp).Tables[0];
         return GetColumnList<Type>(dt);
      }

      public List<Type> GetColumnList<Type>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         StoredProcedure<DataSet> sp = new StoredProcedure<DataSet>(sTable, dbParams.ToList<OleDbParameter>(), Trx);
         return GetColumnList<Type>(sp);
      }

      public List<Type> GetColumnList<Type>(string sTable, List<OleDbParameter> dbParams, OleDbTransaction Trx = null)
      {
         StoredProcedure<DataSet> sp = new StoredProcedure<DataSet>(sTable, dbParams, Trx);
         return GetColumnList<Type>(sp);
      }

      public List<Type> GetColumnList<Type>(DataTable dt, int RowIndex = 0)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < dt.Columns.Count; i++)
         {
            list.Add((Type)dt.Rows[RowIndex][i]);
         }
         return list;
      }

      #endregion " Columns List Method "

      #region " Rows List Method "

      public List<Type> GetRowList<Type>(IStoredProcedure<DataSet> sp)
      {
         DataTable dt = Execute<DataSet>(sp).Tables[0];
         return GetRowList<Type>(dt);
      }

      public List<Type> GetRowList<Type>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null)
      {
         StoredProcedure<DataSet> sp = new StoredProcedure<DataSet>(sTable, dbParams.ToList<OleDbParameter>(), Trx);
         return GetRowList<Type>(sp);
      }

      public List<Type> GetRowList<Type>(string sTable, List<OleDbParameter> dbParams, OleDbTransaction Trx = null)
      {
         StoredProcedure<DataSet> sp = new StoredProcedure<DataSet>(sTable, dbParams, Trx);
         return GetRowList<Type>(sp);
      }

      public List<Type> GetRowList<Type>(DataTable dt, string ColumnName)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < dt.Rows.Count; i++)
         {
            list.Add((Type)dt.Rows[0][ColumnName]);
         }
         return list;
      }

      public List<Type> GetRowList<Type>(DataTable dt, int ColumnIndex = 0)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < dt.Rows.Count; i++)
         {
            list.Add((Type)dt.Rows[0][ColumnIndex]);
         }
         return list;
      }

		#endregion " Rows List Method "

		#region " Create Parameter Methods"

		public List<OleDbParameter> CreateParameter(DataTable dt) => DB.CreateParameters(dt);

      public List<OleDbParameter> CreateParameter(Dictionary<string, Type, object> dic) => DB.CreateParameters(dic);

      public OleDbParameter CreateParameter(string name, object value)
      {
         return DB.CreateParameters(name, value);
      }

      public OleDbParameter CreateParameter(string name, object value, OleDbType dataType)
      {
         return DB.CreateParameters(name, value, dataType);
      }

      public OleDbParameter CreateParameter(string name, OleDbType dataType, JToken value)
      {
         return DB.CreateParameters(name, dataType, value);
      }

      public OleDbParameter CreateParameter(string name, OleDbType dataType, JToken value, object defValue)
      {
         return DB.CreateParameters(name, dataType, value, defValue);
      }

      public static OleDbParameter CreateParameters(string name, object value)
      {
         OleDbParameter dbParam;
         OleDbType valueDbType = value.ToOleDbType();
         dbParam = new OleDbParameter(name, valueDbType);
         if (value != null)
         {
            if (!(valueDbType == OleDbType.DBDate && (DateTime)value == default(DateTime)))
            {
               dbParam.Value = value;
               return dbParam;
            }
         }

         dbParam.Value = DBNull.Value;
         return dbParam;
      }

      public static OleDbParameter CreateParameters(string name, object value, OleDbType dataType)
      {
         OleDbParameter dbParam;
         dbParam = new OleDbParameter(name, dataType);
         if (value != null)
         {
            try
            {
               if (!(dataType == OleDbType.DBDate && (DateTime)Convert.ChangeType(value, typeof(DateTime)) == default(DateTime)))
               {
                  dbParam.Value = value;
                  return dbParam;
               }
            }
            catch { }
         }

         dbParam.Value = DBNull.Value;
         return dbParam;
      }

      public static OleDbParameter CreateParameters(string name, OleDbType dataType, JToken value)
      {
         return DB.CreateParameters(name, dataType, value, DBNull.Value);
      }

      public static OleDbParameter CreateParameters(string name, OleDbType dataType, JToken value, object defValue)
      {
         OleDbParameter dbParam;
         dbParam = new OleDbParameter(name, dataType);

         if (value != null)
         {
            if (((JValue)value).Value != null)
            {
               if (((JValue)value).Value.ToString() != String.Empty)
               {
                  dbParam.Value = ((JValue)value).Value;
               }
               else
               {
                  switch (dataType)
                  {
                     case OleDbType.Boolean:
                        dbParam.Value = defValue;
                        break;

                     case OleDbType.Integer:
                        dbParam.Value = defValue;
                        break;

                     case OleDbType.Date:
                        dbParam.Value = defValue;
                        break;

                     case OleDbType.DBTime:
                        dbParam.Value = defValue;
                        break;

                     case OleDbType.Currency:
                        dbParam.Value = defValue;
                        break;

                     case OleDbType.Decimal:
                        dbParam.Value = defValue;
                        break;

                     case OleDbType.Double:
                        dbParam.Value = defValue;
                        break;

                     default:
                        dbParam.Value = ((JValue)value).Value;
                        break;
                  }
               }
               return dbParam;
            }
         }

         dbParam.Value = defValue;
         return dbParam;
      }

      public static List<OleDbParameter> CreateParameters(DataTable dt)
      {
         List<OleDbParameter> dbParams;

         dbParams = new List<OleDbParameter>();

         for (int i = 0; i < dt.Columns.Count; i++)
         {
            dbParams.Add(CreateParameters("@" + dt.Columns[i].ColumnName, dt.Rows[0][i]));
         }

         return dbParams;
      }

      public static List<OleDbParameter> CreateParameters(Dictionary<string, Type, object> dic)
      {
         List<OleDbParameter> dbParams;

         dbParams = new List<OleDbParameter>();

         foreach (var item in dic)
         {
            dbParams.Add(CreateParameters("@" + item.Key.Item1, item.Value, item.Key.Item2.ToOleDbType()));
         }

         return dbParams;
      }

      public void FillParameters(OleDbCommand dbCommand, OleDbParameter[] dbParams)
          => ParametersProvider.FillParameters(dbCommand, dbParams);

      public void CleanOleDbParameterCollections()
          => ParametersProvider.CleanOleDbParameterCollections();

      public void Dispose()
      {
      }

      #endregion " Create Parameter Methods"

      #region " Stored Procedures Methods "

      public IStoredProcedure<T> StoredProcedure<T>(string spName, List<OleDbParameter> dbParams, OleDbTransaction dbTrx = null)
      {
         return new StoredProcedure<T>(spName, dbParams, dbTrx);
      }

      public IStoredProcedure<T> StoredProcedure<T>(string spName, OleDbTransaction dbTrx)
      {
         return new StoredProcedure<T>(spName, dbTrx);
      }

      public IStoredProcedure<T> StoredProcedure<T>(string spName)
      {
         return new StoredProcedure<T>(spName);
      }

      #endregion " Stored Procedures Methods "

      #region " Pubic Transactions Methods "

      public void SetTransaction(OleDbTransaction transaction)
      {
         Transaction = transaction;
      }

      public OleDbTransaction GetCurrentTransaction()
      {
         return Transaction;
      }

      public void StartTransaction(bool Async = false)
      {
         Connection = CreateOleDbConnection();
         if (Async)
         {
            Connection.Close();
            Connection.OpenAsync();
         }
         Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
      }

      public OleDbTransaction NewTransaction(bool Async = false)
      {
         Connection = CreateOleDbConnection();
         if (Async)
         {
            Connection.Close();
            Connection.OpenAsync();
         }
         return Connection.BeginTransaction(IsolationLevel.ReadCommitted);
      }

      public void CommitTransaction()
      {
         if (Transaction == null)
            throw new Exception("Transaction is not started");
         CommitTransaction(Transaction);
      }

      public void CommitTransaction(OleDbTransaction trx)
      {
         try
         {
            trx.Commit();
         }
         catch (Exception ex)
         {
            try
            {
               trx.Rollback();
            }
            catch { }
            throw ex;
         }
         finally
         {
            Connection.Close();
            Connection.Dispose();
         }
      }

      public void RollbackTransaction()
      {
         if (Transaction == null)
            throw new Exception("Transaction is not started");
         RollbackTransaction(Transaction);
      }

      public void RollbackTransaction(OleDbTransaction trx)
      {
         try
         {
            trx.Rollback();
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {
            Connection.Close();
            Connection.Dispose();
         }
      }

      #endregion " Pubic Transactions Methods "

      #endregion " Public SQL Methods "
   }
}