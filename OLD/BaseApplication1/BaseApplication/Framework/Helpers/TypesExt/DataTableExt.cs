using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
   public static class DataTableExt
   {
      /// <summary>
      ///   Create a List of Type with all values of all columns from the specific row index
      /// </summary>
      public static List<Type> ColumnsToList<Type>(this DataTable dt, int RowIndex = 0)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < dt.Columns.Count; i++)
         {
            list.Add((Type)dt.Rows[RowIndex][i]);
         }
         return list;
      }

      /// <summary>
      ///   Create a List of Type with all values of all rows from the specific column name
      /// </summary>
      public static List<Type> RowsToList<Type>(this DataTable dt, string ColumnName)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < dt.Rows.Count; i++)
         {
            list.Add((Type)dt.Rows[0][ColumnName]);
         }
         return list;
      }

      /// <summary>
      ///   Create a List of Type with all values of all rows from the specific column index
      /// </summary>
      public static List<Type> RowsToList<Type>(this DataTable dt, int ColumnIndex = 0)
      {
         List<Type> list = new List<Type>();
         for (int i = 0; i < dt.Rows.Count; i++)
         {
            list.Add((Type)dt.Rows[0][ColumnIndex]);
         }
         return list;
      }

      /// <summary>
      ///   Create a List of tuple of two types with all values of all rows from two specific column index
      /// </summary>
      public static List<Tuple<TypeA, TypeB>> RowsToList<TypeA, TypeB>(this DataTable ds, int ColumnIndexA = 0, int ColumnIndexB = 1)
      {
         List<Tuple<TypeA, TypeB>> list = new List<Tuple<TypeA, TypeB>>();
         for (int i = 0; i < ds.Rows.Count; i++)
         {
            list.Add(new Tuple<TypeA, TypeB>((TypeA)ds.Rows[0][ColumnIndexA], (TypeB)ds.Rows[0][ColumnIndexB]));
         }
         return list;
      }

      /// <summary>
      ///   Create a List of tripe-tuple of types with all values of all rows from three specific column index
      /// </summary>
      public static List<Tuple<TypeA, TypeB, TypeC>> RowsToList<TypeA, TypeB, TypeC>(this DataTable ds, int ColumnIndexA = 0, int ColumnIndexB = 1, int ColumnIndexC = 2)
      {
         List<Tuple<TypeA, TypeB, TypeC>> list = new List<Tuple<TypeA, TypeB, TypeC>>();
         for (int i = 0; i < ds.Rows.Count; i++)
         {
            list.Add(new Tuple<TypeA, TypeB, TypeC>((TypeA)ds.Rows[0][ColumnIndexA], (TypeB)ds.Rows[0][ColumnIndexB], (TypeC)ds.Rows[0][ColumnIndexC]));
         }
         return list;
      }

     


   }
}