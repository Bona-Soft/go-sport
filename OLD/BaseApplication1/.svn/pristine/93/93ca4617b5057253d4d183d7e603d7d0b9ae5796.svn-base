using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
   public static class DataSetExt
   {
      /// <summary>
      ///   Generate a List from all columns in the first table of the a specific row. If you have a row with diferent columns it will be listed.
      /// </summary>
      /// <typeparam name="Type">Type of the List. Every column value in the row will be casted (type)value</typeparam>
      /// <param name="ds">The data set that containt a Table[0]</param>
      /// <param name="rowIndex">The row index to be listed as column. Default: 0 (First row) </param>
      /// <returns>A new List of Type containing all the values of all columns of ds.table.row</returns>
      public static List<Type> ColumnsToList<Type>(this DataSet ds, int rowIndex = 0)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
         {
            list.Add((Type)ds.Tables[0].Rows[rowIndex][i]);
         }
         return list;
      }

      /// <summary>
      ///    Generate a List from all rows in the first table of the a specific column. If you have a column with lots of rows it will be listed.
      /// </summary>
      /// <typeparam name="Type">Type of the List. Every row value in the colum will be casted (type)value</typeparam>
      /// <param name="ds">The data set that containt a Table[0] and a Rows[0]</param>
      /// <param name="columnName">The name of the column where rows values will be taken</param>
      /// <returns>A new List of Type containing all the values of all rows of an especific column</returns>
      public static List<Type> RowsToList<Type>(this DataSet ds, string columnName)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {
            list.Add((Type)ds.Tables[0].Rows[0][columnName]);
         }
         return list;
      }

      /// <summary>
      ///    Generate a List from all rows in the first table of the a specific column. If you have a column with lots of rows it will be listed.
      /// </summary>
      /// <typeparam name="Type">Type of the List. Every row value in the colum will be casted (type)value</typeparam>
      /// <param name="ds">The data set that containt a Table[0] and a Rows[0]</param>
      /// <param name="columnIndex">The index of the column where rows values will be taken</param>
      /// <returns>A new List of Type containing all the values of all rows of an especific column</returns>
      public static List<Type> RowsToList<Type>(this DataSet ds, int columnIndex = 0)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {
            list.Add((Type)ds.Tables[0].Rows[0][columnIndex]);
         }
         return list;
      }

      /// <summary>
      ///    Generate a List of tuples from all rows in the first table of 2 column. If you have a column with lots of rows it will be listed.
      /// </summary>
      /// <typeparam name="Type">Type of the List. Every row value in the colum will be casted (type)value</typeparam>
      /// <param name="ds">The data set that containt a Table[0] and a Rows[0]</param>
      /// <param name="columnIndex">The index of the column where rows values will be taken</param>
      /// <returns>A new List of tuples TypeA and TypeB containing all the values of all rows of two especific column</returns>
      public static List<Tuple<TypeA, TypeB>> RowsToList<TypeA, TypeB>(this DataSet ds, int columnIndexA = 0, int columnIndexB = 1)
      {
         List<Tuple<TypeA, TypeB>> list = new List<Tuple<TypeA, TypeB>>();
         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {
            list.Add(new Tuple<TypeA, TypeB>((TypeA)ds.Tables[0].Rows[0][columnIndexA], (TypeB)ds.Tables[0].Rows[0][columnIndexB]));
         }
         return list;
      }

      /// <summary>
      ///   Generate a List of tuples from all rows in the first table of 2 column. If you have a column with lots of rows it will be listed.
      /// </summary>
      /// <typeparam name="Type">Type of the List. Every row value in the colum will be casted (type)value</typeparam>
      /// <param name="ds">The data set that containt a Table[0] and a Rows[0]</param>
      /// <param name="columnName">The name of the column where rows values will be taken</param>
      /// <returns>A new List of triple-tuples TypeA, TypeB and TypeC containing all the values of all rows of two especific column</returns>
      public static List<Tuple<TypeA, TypeB, TypeC>> RowsToList<TypeA, TypeB, TypeC>(this DataSet ds, string columnNameA, string columnNameB, string columnNameC)
      {
         List<Tuple<TypeA, TypeB, TypeC>> list = new List<Tuple<TypeA, TypeB, TypeC>>();
         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {
            list.Add(new Tuple<TypeA, TypeB, TypeC>((TypeA)ds.Tables[0].Rows[0][columnNameA], (TypeB)ds.Tables[0].Rows[0][columnNameB], (TypeC)ds.Tables[0].Rows[0][columnNameC]));
         }
         return list;
      }

      /// <summary>
      ///   Generate a List of tuples from all rows in the first table of 2 column. If you have a column with lots of rows it will be listed.
      /// </summary>
      /// <typeparam name="Type">Type of the List. Every row value in the colum will be casted (type)value</typeparam>
      /// <param name="ds">The data set that containt a Table[0] and a Rows[0]</param>
      /// <param name="columnIndex">The index of the column where rows values will be taken</param>
      /// <returns>A new List of triple-tuples TypeA, TypeB and TypeC containing all the values of all rows of two especific column</returns>
      public static List<Tuple<TypeA, TypeB, TypeC>> RowsToList<TypeA, TypeB, TypeC>(this DataSet ds, int columnIndexA = 0, int columnIndexB = 1, int columnIndexC = 2)
      {
         List<Tuple<TypeA, TypeB, TypeC>> list = new List<Tuple<TypeA, TypeB, TypeC>>();
         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {
            list.Add(new Tuple<TypeA, TypeB, TypeC>((TypeA)ds.Tables[0].Rows[0][columnIndexA], (TypeB)ds.Tables[0].Rows[0][columnIndexB], (TypeC)ds.Tables[0].Rows[0][columnIndexC]));
         }
         return list;
      }

      #region " To Dictionary "

      /// <summary>
      ///   Returns a new Dictionary with the data of the DataSet with the column named "columnKeyName" as key and the column named "columnFieldName" as the values
      /// </summary>
      public static Dictionary<TKey, TValue> RowToDic<TKey, TValue>(this DataSet ds, string columnKeyName, string columnFieldName)
      {
         Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
         foreach (DataRow row in ds.Tables[0].Rows)
         {
            dic.Add((TKey)row[columnKeyName], (TValue)row[columnFieldName]);
         }
         return dic;
      }

      /// <summary>
      ///   Returns a new Dictionary with the data of the DataSet with the column index "columnIndexKey" as key and the column index "columnIndexValue" as the values
      /// </summary>
      public static Dictionary<TKey, TValue> RowToDic<TKey, TValue>(this DataSet ds, int columnIndexKey = 0, int columnIndexValue = 1)
      {
         Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
         foreach (DataRow row in ds.Tables[0].Rows)
         {
            dic.Add((TKey)row[columnIndexKey], (TValue)row[columnIndexValue]);
         }
         return dic;
      }


      /// <summary>
      ///   Returns a new doble key Dictionary with the data of the DataSet with the columns name "columnNameKey1" and "columnNameKey2" as keys and the column name "columnNameValue" as the values
      /// </summary>
      public static Dictionary<TKey1, TKey2, TValue> RowToDic<TKey1, TKey2, TValue>(this DataSet ds, string columnNameKey1, string columnNameKey2, string columnNameValue)
      {
         Dictionary<TKey1, TKey2, TValue> dic = new Dictionary<TKey1, TKey2, TValue>();
         foreach (DataRow row in ds.Tables[0].Rows)
         {
            dic.Add((TKey1)row[columnNameKey1], (TKey2)row[columnNameKey2], (TValue)row[columnNameValue]);
         }
         return dic;
      }

      /// <summary>
      ///   Returns a new doble key Dictionary with the data of the DataSet with the columns index "columnIndexKey1" and "columnIndexKey2" as keys and the column index "columnIndexValue" as the values
      /// </summary>
      public static Dictionary<TKey1, TKey2, TValue> RowToDic<TKey1, TKey2, TValue>(this DataSet ds, int columnIndexKey1 = 0, int columnIndexKey2 = 1, int columnIndexValue = 2)
      {
         Dictionary<TKey1, TKey2, TValue> dic = new Dictionary<TKey1, TKey2, TValue>();
         foreach (DataRow row in ds.Tables[0].Rows)
         {
            dic.Add((TKey1)row[columnIndexKey1], (TKey2)row[columnIndexKey2], (TValue)row[columnIndexValue]);
         }
         return dic;
      }

      /// <summary>
      ///   Create a new dictionary with a specific row values for each column, with the name of the columns as keys.
      /// </summary>
      /// <param name="rowIndex">The number of the row to be taked as TValue object. Default: 0 (First)</param>
      /// <returns>A new dictionary representing the columns name of the dataset as string key and the specific row value as (TValue)value </returns>
      public static Dictionary<string, TValue> ColumnsToDic<TValue>(this DataSet ds, int rowIndex = 0)
      {
         Dictionary<string, TValue> dic = new Dictionary<string, TValue>();
         foreach (DataColumn column in ds.Tables[0].Columns)
         {
            dic.Add(column.ColumnName, (TValue)ds.Tables[0].Rows[rowIndex][column.ColumnName]);
         }
         return dic;
      }
      #endregion " To Dictionary "

      #region " To Escalar "

      /// <summary>
      ///   Convert the index "rowIndex" element value of a specific column index "columnIndex" of the dataset to the type T. 
      /// </summary>
      public static T ToEscalar<T>(this DataSet ds, int rowIndex = 0, int columnIndex = 0)
      {
         return (T)ds.Tables[0].Rows[rowIndex][columnIndex];
      }

      /// <summary>
      ///   Convert the index "rowIndex"  value of the dataset to the type T. 
      /// </summary>
      public static T ToEscalar<T>(this DataSet ds, string columnName, int rowIndex = 0)
      {
         return (T)ds.Tables[0].Rows[rowIndex][columnName];
      }

      #endregion " To Escalar "
   }
}