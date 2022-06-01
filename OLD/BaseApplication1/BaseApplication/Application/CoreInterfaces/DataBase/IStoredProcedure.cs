using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IStoredProcedure<T>
	{
		string Name { get; set; }
		List<OleDbParameter> Params { get; set; }
		OleDbTransaction Trx { get; set; }

		T ExecuteScalar<T>(IDataBase pDB) where T : IEquatable<string>, IEquatable<int>, IEquatable<short>, IEquatable<long>;

		T Execute(IDataBase pDB);
	}
}