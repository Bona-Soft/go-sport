using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MYB.BaseApplication.Framework.Helpers
{
   public class GenericInstance
   {
      /// <summary>
      ///   Get the default value of the generic method of a type.
      /// </summary>
      public object GetDefault(Type t)
      {
         return GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(this, null);
      }

      /// <summary>
      ///   Get the default value of a generic type
      /// </summary>
      public T GetDefaultGeneric<T>()
      {
         return default(T);
      }
   }

   public static class Generics
   {
      private static GenericInstance _Instance;

      private static GenericInstance Instance
      {
         get
         {
            if (_Instance == null)
            {
               _Instance = new GenericInstance();
            }
            return _Instance;
         }
      }

      /// <summary>
      ///   Get the default value of the generic method of a type.
      /// </summary>
      public static object GetDefault(Type t)
      {
         return Instance.GetDefault(t);
      }

      /// <summary>
      ///   Invoke a generic mehtod. i.e. "InvokeGeneric-YourClass,bool-("YourMethod", --a new list with typeof(AnotherClass) added--, long ID);" this is the same that do. YourClassInstance.YourMethod<AnotherClass>(ID);
      /// </summary>
      /// <typeparam name="MethodClassType">The class that contains the method </typeparam>
      /// <typeparam name="ReturnType">The type of the return value of the method </typeparam>
      /// <param name="methodName">The name of the method </param>
      /// <param name="typeList">The type list of the generic type of the method </param>
      /// <param name="argument">Arguments of the invoked method</param>
      /// <returns>The return value of the invoked method</returns>
      public static ReturnType InvokeGeneric<MethodClassType, ReturnType>(string methodName, List<Type> typeList, object[] argument)
      {
         MethodInfo method = typeof(MethodClassType).GetMethod(methodName);
         MethodInfo generic = method.MakeGenericMethod(typeList.ToArray());
         return (ReturnType)generic.Invoke(null, new object[] { argument });
      }

      /// <summary>
      ///   Return the obj casted as T or the default value of an object if it is null or dbnull
      /// </summary>
      private static T DefValue<T>(object obj, T defValue)
      {
         if (obj == null || obj == DBNull.Value)
         {
            return defValue;
         }
         return (T)obj;
      }

      /// <summary>
      ///   True if the type is a primitive. i.e. string, datetime, enum.
      /// </summary>
      public static bool IsSimpleType(Type type)
      {
         bool a;
         try
         {
            a = type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(DateTime?);
         }
         catch
         {
            return false;
         }
         return a;
      }

      public static class GetSQLServerStrings
      {
         /// <summary>
         ///   Returns a SQL Server query that create a table, the get and update procedure from an entity by reflection. 
         /// </summary>
         public static string FullTableFromEntity<TEntity>(TEntity entity, string chema = "[dbo]")
         {
            string query = "";
            query += CreateTableFromEntity(entity, chema);
            query += Environment.NewLine + " GO " + Environment.NewLine;
            query += ProcGetTableFromEntity(entity, chema);
            query += Environment.NewLine + " GO " + Environment.NewLine;
            query += ProcUpdateTableFromEntity(entity, chema);
            return query;
         }

         /// <summary>
         ///   Returns a SQL Server query that create a table from an entity by reflection. 
         /// </summary>
         public static string CreateTableFromEntity<TEntity>(TEntity entity, string chema = "[dbo]")
         {
            string query = "";
            if (entity != null)
            {
               string tableName = entity.GetType().Name;
               string pkName = null;
               Dictionary<string, Type, object> dicProperties = Key.EntityToDictionary(entity);
               if (dicProperties.Count == 0)
                  return "";
               foreach (var item in entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
               {
                  if (item.PropertyType.GetNameWithoutGenericArity() == "key")
                  {
                     pkName = item.Name;
                     break;
                  }
               }
               if (chema == null) chema = "";
               query = "CREATE TABLE " + chema + (chema == "" ? "" : ".") + "[" + tableName + "] " + Environment.NewLine + " \t(";

               foreach (var prop in dicProperties)
               {
                  if (pkName == null)
                     pkName = prop.Key.Item1;
                  query += "[" + prop.Key.Item1 + "] " + prop.Key.Item2.ToOleDbType().ToString().ToUpper() + (pkName == prop.Key.Item1 ? " NOT NULL" : " NULL") + ", " + Environment.NewLine + "\t";
               }
               query += "CONSTRAINT[PK_" + tableName + "] PRIMARY KEY([" + pkName + "]))" + Environment.NewLine;
            }

            query = query.Replace("DBDATE", "DATETIME");
            return query;
         }

         /// <summary>
         ///   Returns a SQL Server query that create the get procedure from an entity by reflection. 
         /// </summary>
         public static string ProcGetTableFromEntity<TEntity>(TEntity entity, string chema = "[dbo]")
         {
            string query = "";
            if (entity != null)
            {
               string tableName = entity.GetType().Name;
               Dictionary<string, Type, object> dicProperties = Key.EntityToDictionary(entity);

               if (dicProperties.Count == 0)
                  return "";

               if (chema == null)
                  chema = "";

               query = "CREATE PROCEDURE " + chema + (chema == "" ? "" : ".") + "[Get" + tableName + "] " + Environment.NewLine;

               foreach (var prop in dicProperties)
               {
                  query += "\t@" + prop.Key.Item1 + " " + prop.Key.Item2.ToOleDbType().ToString().ToUpper() + " = NULL, " + Environment.NewLine;
               }
               query = query.Remove(query.Length - 4);
               query += Environment.NewLine;

               query += "AS " + Environment.NewLine + "SELECT " + Environment.NewLine;
               foreach (var prop in dicProperties)
               {
                  query += "\t[" + prop.Key.Item1 + "], " + Environment.NewLine;
               }
               query = query.Remove(query.Length - 4);
               query += Environment.NewLine;

               query += "FROM " + tableName + " " + Environment.NewLine + "WHERE " + Environment.NewLine;
               foreach (var prop in dicProperties)
               {
                  query += "\t[" + prop.Key.Item1 + "] = ISNULL(@" + prop.Key.Item1 + "," + prop.Key.Item1 + ") AND " + Environment.NewLine;
               }
               query = query.Remove(query.Length - 6);
               query += Environment.NewLine;
            }

            query = query.Replace("DBDATE", "DATETIME");

            return query;
         }

         /// <summary>
         ///   Returns a SQL Server query that create the update procedure from an entity by reflection. 
         /// </summary>
         public static string ProcUpdateTableFromEntity<TEntity>(TEntity entity, string chema = "[dbo]")
         {
            string query = "";
            if (entity != null)
            {
               string tableName = entity.GetType().Name;
               Dictionary<string, Type, object> dicProperties = Key.EntityToDictionary(entity);
               string pkName = null;

               if (dicProperties.Count == 0)
                  return "";

               foreach (var item in entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
               {
                  if (item.PropertyType.GetNameWithoutGenericArity() == "key")
                  {
                     pkName = item.Name;
                     break;
                  }
               }

               if (chema == null)
                  chema = "";

               query = "CREATE PROCEDURE " + chema + (chema == "" ? "" : ".") + "[Update" + tableName + "] " + Environment.NewLine;

               foreach (var prop in dicProperties)
               {
                  query += "\t@" + prop.Key.Item1 + " " + prop.Key.Item2.ToOleDbType().ToString().ToUpper() + " = NULL, " + Environment.NewLine;
               }
               query = query.Remove(query.Length - 4);
               query += Environment.NewLine;

               query += "AS " + Environment.NewLine + "UPDATE " + tableName + Environment.NewLine + "SET " + Environment.NewLine;
               foreach (var prop in dicProperties)
               {
                  if (prop.Key.Item1 != pkName)
                     query += "\t[" + prop.Key.Item1 + "] = @" + prop.Key.Item1 + "," + Environment.NewLine;
               }
               query = query.Remove(query.Length - 3);
               query += Environment.NewLine;

               query += "WHERE " + Environment.NewLine + "\t" + pkName + " = @" + pkName;
               query += Environment.NewLine;
            }

            query = query.Replace("DBDATE", "DATETIME");

            return query;
         }
      }
   }
}