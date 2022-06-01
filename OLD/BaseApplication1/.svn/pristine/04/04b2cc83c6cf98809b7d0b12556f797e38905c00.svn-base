using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace MYB.BaseApplication.Framework.Helpers
{
	public static class TypeExtension
	{

		public static SqlDbType ToSqlDbType(this Type typeObj)
		{
			//Classics
			if (typeObj == typeof(String)) return SqlDbType.VarChar;
			if (typeObj == typeof(Int32)) return SqlDbType.Int;
			if (typeObj == typeof(Boolean)) return SqlDbType.Bit;
			if (typeObj == typeof(Decimal)) return SqlDbType.Decimal;
			if (typeObj == typeof(DateTime)) return SqlDbType.DateTime;
			if (typeObj == typeof(Char)) return SqlDbType.Char;
			if (typeObj == typeof(Object)) return SqlDbType.Variant;
			if (typeObj == typeof(Byte)) return SqlDbType.Binary;

			//Classics Nulls
			if (typeObj == typeof(Int32?)) return SqlDbType.Int;
			if (typeObj == typeof(Boolean?)) return SqlDbType.Bit;
			if (typeObj == typeof(Decimal?)) return SqlDbType.Decimal;
			if (typeObj == typeof(DateTime?)) return SqlDbType.DateTime;
			if (typeObj == typeof(Char?)) return SqlDbType.Char;
			if (typeObj == typeof(Byte?)) return SqlDbType.Binary;

			//Other used
			if (typeObj == typeof(Int16)) return SqlDbType.SmallInt;
			if (typeObj == typeof(UInt16)) return SqlDbType.Int;
			if (typeObj == typeof(UInt32)) return SqlDbType.BigInt;
			if (typeObj == typeof(Int64)) return SqlDbType.BigInt;
			if (typeObj == typeof(UInt64)) return SqlDbType.Real;
			if (typeObj == typeof(Single)) return SqlDbType.Float;
			if (typeObj == typeof(Double)) return SqlDbType.Float;
			if (typeObj == typeof(TimeSpan)) return SqlDbType.Timestamp;
			if (typeObj == typeof(Exception)) return SqlDbType.Variant;
			if (typeObj == typeof(Guid)) return SqlDbType.NVarChar;
			if (typeObj == typeof(SByte)) return SqlDbType.Binary;

			return default(SqlDbType);
		}

        public static SqlDbType ToSqlDbType(this object clrType)
		{
			Type tp = clrType.GetType();
			return tp.ToSqlDbType();
		}

		public static OleDbType ToOleDbType(this Type typeObj)
		{
			//Classics
			if (typeObj == typeof(String)) return OleDbType.VarChar;
			if (typeObj == typeof(Int32)) return OleDbType.Integer;
			if (typeObj == typeof(Boolean)) return OleDbType.Boolean;
			if (typeObj == typeof(Decimal)) return OleDbType.Decimal;
			if (typeObj == typeof(DateTime)) return OleDbType.DBDate;
			if (typeObj == typeof(Char)) return OleDbType.Char;
			if (typeObj == typeof(Object)) return OleDbType.Variant;
			if (typeObj == typeof(Byte)) return OleDbType.Binary;

			//Classics
			if (typeObj == typeof(Int32?)) return OleDbType.Integer;
			if (typeObj == typeof(Boolean?)) return OleDbType.Boolean;
			if (typeObj == typeof(Decimal?)) return OleDbType.Decimal;
			if (typeObj == typeof(DateTime?)) return OleDbType.DBDate;
			if (typeObj == typeof(Char?)) return OleDbType.Char;
			if (typeObj == typeof(Byte?)) return OleDbType.Binary;

			//Other used
			if (typeObj == typeof(Int16)) return OleDbType.SmallInt;
			if (typeObj == typeof(UInt16)) return OleDbType.UnsignedSmallInt;
			if (typeObj == typeof(UInt32)) return OleDbType.UnsignedInt;
			if (typeObj == typeof(Int64)) return OleDbType.BigInt;
			if (typeObj == typeof(UInt64)) return OleDbType.UnsignedBigInt;
			if (typeObj == typeof(Single)) return OleDbType.Single;
			if (typeObj == typeof(Double)) return OleDbType.Double;
			if (typeObj == typeof(TimeSpan)) return OleDbType.DBTime;
			if (typeObj == typeof(Exception)) return OleDbType.Error;
			if (typeObj == typeof(Guid)) return OleDbType.Guid;
			if (typeObj == typeof(SByte)) return OleDbType.TinyInt;

			//Not used - Not enough types in c#
			if (typeObj == typeof(String)) return OleDbType.BSTR;
			if (typeObj == typeof(String)) return OleDbType.LongVarChar;
			if (typeObj == typeof(String)) return OleDbType.LongVarWChar;
			if (typeObj == typeof(String)) return OleDbType.VarWChar;
			if (typeObj == typeof(String)) return OleDbType.WChar;
			if (typeObj == typeof(Decimal)) return OleDbType.Currency;
			if (typeObj == typeof(Decimal)) return OleDbType.Numeric;
			if (typeObj == typeof(Decimal)) return OleDbType.VarNumeric;
			if (typeObj == typeof(DateTime)) return OleDbType.Date;
			if (typeObj == typeof(DateTime)) return OleDbType.DBTimeStamp;
			if (typeObj == typeof(DateTime)) return OleDbType.Filetime;
			if (typeObj == typeof(Object)) return OleDbType.IDispatch;
			if (typeObj == typeof(Object)) return OleDbType.PropVariant;
			if (typeObj == typeof(Byte)) return OleDbType.LongVarBinary;
			if (typeObj == typeof(Byte)) return OleDbType.UnsignedTinyInt;

			return OleDbType.IUnknown;
		}

		public static OleDbType ToOleDbType(this object clrObjValue)
		{
			if (clrObjValue == null) return OleDbType.Empty;
			if (clrObjValue.GetType() == typeof(DBNull)) return OleDbType.Empty;
			if (clrObjValue.GetType().GetType() == typeof(String)) return OleDbType.VarChar;

			return clrObjValue.GetType().ToOleDbType();
		}

		public static Type ToClrType(OleDbType oleType)
		{
			switch (oleType)
			{
				case OleDbType.BigInt:
					return typeof(long?);

				case OleDbType.Binary:
				case OleDbType.DBTimeStamp:
				case OleDbType.VarBinary:
					return typeof(byte[]);

				case OleDbType.Char:
				case OleDbType.WChar:
				case OleDbType.VarChar:
					return typeof(string);

				case OleDbType.DBDate:
				case OleDbType.Date:
					return typeof(DateTime?);

				case OleDbType.Decimal:
				case OleDbType.Currency:
					return typeof(decimal?);

				case OleDbType.Double:
					return typeof(double?);

				case OleDbType.Integer:
					return typeof(int?);

				case OleDbType.SmallInt:
					return typeof(short?);

				case OleDbType.TinyInt:
					return typeof(byte?);

				case OleDbType.Variant:

				default:
					throw new ArgumentOutOfRangeException("sqlType");
			}
		}

		public static Type ToClrType(SqlDbType sqlType)
		{
			switch (sqlType)
			{
				case SqlDbType.BigInt:
					return typeof(long?);

				case SqlDbType.Binary:
				case SqlDbType.Image:
				case SqlDbType.Timestamp:
				case SqlDbType.VarBinary:
					return typeof(byte[]);

				case SqlDbType.Bit:
					return typeof(bool?);

				case SqlDbType.Char:
				case SqlDbType.NChar:
				case SqlDbType.NText:
				case SqlDbType.NVarChar:
				case SqlDbType.Text:
				case SqlDbType.VarChar:
				case SqlDbType.Xml:
					return typeof(string);

				case SqlDbType.DateTime:
				case SqlDbType.SmallDateTime:
				case SqlDbType.Date:
				case SqlDbType.Time:
				case SqlDbType.DateTime2:
					return typeof(DateTime?);

				case SqlDbType.Decimal:
				case SqlDbType.Money:
				case SqlDbType.SmallMoney:
					return typeof(decimal?);

				case SqlDbType.Float:
					return typeof(double?);

				case SqlDbType.Int:
					return typeof(int?);

				case SqlDbType.Real:
					return typeof(float?);

				case SqlDbType.UniqueIdentifier:
					return typeof(Guid?);

				case SqlDbType.SmallInt:
					return typeof(short?);

				case SqlDbType.TinyInt:
					return typeof(byte?);

				case SqlDbType.Variant:
				case SqlDbType.Udt:
					return typeof(object);

				case SqlDbType.Structured:
					return typeof(DataTable);

				case SqlDbType.DateTimeOffset:
					return typeof(DateTimeOffset?);

				default:
					throw new ArgumentOutOfRangeException("sqlType");
			}
		}
	}
}