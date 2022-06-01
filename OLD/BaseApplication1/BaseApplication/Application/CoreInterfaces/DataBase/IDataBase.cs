using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces.DataBase
{
   public interface IDataBase : IPerWebRequest, IDisposable
   {
      #region " PROPERTIES "

      IDBDataReaderMethods DataReaderGroup { get; }
      IDBDataSetMethods DataSetGroup { get; }
      IDBDataTableMethods DataTableGroup { get; }
      IDBObjectMethods DataObjectGroup { get; }
      bool spUseDerivedParameters { get; set; }

      IConnectionData ConnectionData { get; }

      #endregion " PROPERTIES "

      #region " DINAMIC METHOD RETURNED "

      T Add<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      T Delete<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      T Update<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      T Get<T>(string sAction, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      T Emun<T>(string sTable, OleDbTransaction Trx = null);

      T GetOne<T>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      T GetAll<T>(string sTable, OleDbTransaction Trx = null);

      T Execute<T>(string sSqlCmd, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      T Execute<T>(string sSqlCmd, OleDbTransaction Trx = null);

      T Execute<T>(IStoredProcedure<T> SP);

		#endregion " DINAMIC METHOD RETURNED "

		#region " ESCALAR METHOD RETURNED "

		/// <summary>
		///   Get the first row and column of a table simulating get an escalar value int or string
		/// </summary>
		/// <typeparam name="Type"> Returned object type int or string </typeparam>
		/// <param name="sp"> The store procedure fill with the name and params </param>
		/// <returns> Represent the store procedure table first row and column </returns>
		Type ExecuteScalar<Type>(IStoredProcedure<DataSet> sp) where Type : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>;

		Type ExecuteScalar<Type>(IStoredProcedure<DataTable> sp) where Type : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>;




		/// <summary>
		///   Get the first row and column of a table simulating get an escalar value int or string
		/// </summary>
		/// <typeparam name="Type"> Returned object type int or string </typeparam>
		/// <param name="sTable"> The store procedure name that usually represent a table where get the scalar value</param>
		/// <param name="arrDbParams"> The array of parameters whom the stored procedure will be execute </param>
		/// <param name="Trx"> Optional the transaction, must be commited and closed manually after the method return </param>
		/// <returns> Represent the store procedure table first row and column </returns>
		Type ExecuteScalar<Type>(string sTable, OleDbParameter[] arrDbParams, OleDbTransaction Trx = null) where Type : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>;

		
		#endregion " ESCALAR METHOD RETURNED "

		#region " CREATE PARAMETER METHODS "

		List<OleDbParameter> CreateParameter(DataTable dt);

      List<OleDbParameter> CreateParameter(Dictionary<string, Type, object> dic);

      OleDbParameter CreateParameter(string name, object value);

      OleDbParameter CreateParameter(string name, object value, OleDbType dataType);

      OleDbParameter CreateParameter(string name, OleDbType dataType, JToken value);

      OleDbParameter CreateParameter(string name, OleDbType dataType, JToken value, object defValue);

      void FillParameters(OleDbCommand dbCommand, OleDbParameter[] dbParams);

      void CleanOleDbParameterCollections();

      #endregion " CREATE PARAMETER METHODS "

      #region " Columns List Method "

      List<Type> GetColumnList<Type>(IStoredProcedure<DataSet> sp);

      List<Type> GetColumnList<Type>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      List<Type> GetColumnList<Type>(string sTable, List<OleDbParameter> dbParams, OleDbTransaction Trx = null);

      List<Type> GetColumnList<Type>(DataTable dt, int RowIndex = 0);

      #endregion " Columns List Method "

      #region " Rows List Method "

      List<Type> GetRowList<Type>(IStoredProcedure<DataSet> sp);

      List<Type> GetRowList<Type>(string sTable, OleDbParameter[] dbParams, OleDbTransaction Trx = null);

      List<Type> GetRowList<Type>(string sTable, List<OleDbParameter> dbParams, OleDbTransaction Trx = null);

      List<Type> GetRowList<Type>(DataTable dt, string ColumnName);

      List<Type> GetRowList<Type>(DataTable dt, int ColumnIndex = 0);

      #endregion " Rows List Method "

      #region " STORED PROCEDURE METHODS "

      IStoredProcedure<T> StoredProcedure<T>(string spName, List<OleDbParameter> dbParams, OleDbTransaction dbTrx = null);

      IStoredProcedure<T> StoredProcedure<T>(string spName, OleDbTransaction dbTrx);

      IStoredProcedure<T> StoredProcedure<T>(string spName);

      #endregion " STORED PROCEDURE METHODS "

      #region " Transactions Methods "

      void SetTransaction(OleDbTransaction transaction);

      OleDbTransaction GetCurrentTransaction();

      void StartTransaction(bool Async = false);

      void CommitTransaction();

      void CommitTransaction(OleDbTransaction trx);

      void RollbackTransaction();

      void RollbackTransaction(OleDbTransaction trx);

      OleDbTransaction NewTransaction(bool Async = false);

      #endregion " Transactions Methods "
   }
}