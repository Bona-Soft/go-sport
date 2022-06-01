using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace MYB.BaseApplication.Infrastructure.DB
{
	public class StoredProcedure<T> : IStoredProcedure<T>
	{
		public string Name { get; set; }
		public List<OleDbParameter> Params { get; set; }
		public OleDbTransaction Trx { get; set; }

		/// <summary>
		///   StoreProcedure for MSSQL
		/// </summary>
		/// <param name="spName"> Store procedure name</param>
		/// <param name="dbParams"> Store procedure parameters</param>
		public StoredProcedure(string spName, List<OleDbParameter> dbParams, OleDbTransaction dbTrx = null)
		{
			Name = spName;
			Params = dbParams;
			Trx = dbTrx;
		}

		public StoredProcedure(string spName, OleDbTransaction dbTrx)
		{
			Name = spName;
			Params = null;
			Trx = dbTrx;
		}

		public StoredProcedure(string spName)
		{
			Name = spName;
			Params = null;
			Trx = null;
		}

		public T Execute(IDataBase pDB)
		{
			return pDB.Execute<T>(Name, Params.ToArray(), Trx);
		}

		public T ExecuteScalar<T>(IDataBase pDB) where T : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>
		{
			return pDB.ExecuteScalar<T>(Name, Params.ToArray(), Trx);
		}

		public List<T> GetColumnList(IDataBase pDB)
		{
			return pDB.GetColumnList<T>(Name, Params.ToArray(), Trx);
		}

		public List<T> GetRowList(IDataBase pDB)
		{
			return pDB.GetRowList<T>(Name, Params.ToArray(), Trx);
		}
	}
}