using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace Helpers
{
	//TODO: Pasar todo el keyhelpers al base
	public static class KeyHelpers
	{
		//TODO: Pasar a Base TypeExtensions DataRow,DataTable,DataSet
		public static void FillEntity<T>(T entity,DataTable dt)
		{
			BindingFlags reflectionFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly;

			foreach (var item in entity.GetType().GetProperties(reflectionFlags))
			{
				if(Generics.IsSimpleType(item.PropertyType))
				{
					try
					{
						item.SetValue(entity, dt.Rows[0][item.Name]);
					}
					catch { }
				}
			}
		}

		public static Dictionary<string, Type, object> EntityToDictionary(object entity)
		{
			Dictionary<string, Type, object> dic = new Dictionary<string, Type, object>();

			BindingFlags reflectionFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly;

			foreach (var item in entity.GetType().GetProperties(reflectionFlags))
			{
				if (!item.PropertyType.IsArray
					&& (!item.PropertyType.IsGenericType
						|| item.PropertyType.GetNameWithoutGenericArity() == "key"))
				{
					if (!(item.PropertyType.GetNameWithoutGenericArity() == "key")
						&& !Generics.IsSimpleType(item.PropertyType))
					{
						object subItemInstance = item.GetValue(entity, null);

						if (subItemInstance == null)
						{
							foreach (var subItem2 in item.PropertyType.GetProperties(reflectionFlags))
							{
								if (subItem2.PropertyType.GetNameWithoutGenericArity() == "key")
								{
									//TODO: Obtener generic arity de Key<T>
									dic.Add(
										subItem2.Name, 
										Type.GetType(subItem2.PropertyType.GenericTypeArguments[0].FullName),
										null);
									break;
								}
							}
							continue;
						}
						else
						{
							//TODO: obtener el nombre de la variable key del objeto sin que este instanciado. Ex: ILocation = null sacar LocationID
							foreach (var item2 in subItemInstance.GetType().GetProperties(reflectionFlags))
							{
								if (item2.PropertyType.GetNameWithoutGenericArity() == "key")
								{
									dic.Add(
										item2.Name,
										Type.GetType(item2.PropertyType.GenericTypeArguments[0].FullName),
										subItemInstance.GetType().GetProperty(item2.Name).GetValue(subItemInstance, null).GetType().GetProperty("Value").GetValue(subItemInstance.GetType().GetProperty(item2.Name).GetValue(subItemInstance, null), null)
									);
									break;
								}
							}
						}
					}
					else if (item.PropertyType.GetNameWithoutGenericArity() == "key")
					{
						dic.Add(
							item.Name,
							Type.GetType(item.PropertyType.GenericTypeArguments[0].FullName), 
							entity.GetType().GetProperty(item.Name).GetValue(entity, null).GetType().GetProperty("Value").GetValue(entity.GetType().GetProperty(item.Name).GetValue(entity, null), null)
						); 
					}
					else
					{
						dic.Add(
							item.Name,
							Type.GetType(item.PropertyType.FullName),
							item.GetValue(entity, null)
						);
					}
				}
			}

			return dic;
		}

		public static List<OleDbParameter> CreateParams(DataTable dt)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			for (int i = 0; i < dt.Columns.Count; i++)
			{
				dbParams.Add(BaseApp.DB.CreateParameter("@" + dt.Columns[i].ColumnName, dt.Rows[0][i]));
			}

			return dbParams;
		}

		public static List<OleDbParameter> CreateParams(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			foreach(var item in dic)
			{
				dbParams.Add(BaseApp.DB.CreateParameter("@" + item.Key.Item1, item.Value, item.Key.Item2.ToOleDbType()));
			}

			return dbParams;
		}

		public static DataTable EntityToDataTable(object entity)
		{
			DataTable dt = new DataTable();
			dt.Rows.Add();
			BindingFlags reflectionFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly;

			foreach (var item in entity.GetType().GetProperties(reflectionFlags))
			{
				if (!item.PropertyType.IsArray
					&& (!item.PropertyType.IsGenericType
						|| item.PropertyType.GetNameWithoutGenericArity() == "key"))
				{
					if (!(item.PropertyType.GetNameWithoutGenericArity() == "key")
						&& !Generics.IsSimpleType(item.PropertyType))
					{
						object subItemInstance = item.GetValue(entity, null);
						
						if(subItemInstance == null)
						{
							foreach(var subItem2 in item.PropertyType.GetProperties(reflectionFlags))
							{
								if (subItem2.PropertyType.GetNameWithoutGenericArity() == "key")
								{
									dt.Columns.Add(subItem2.Name);
									dt.Rows[0][subItem2.Name] = null;
									break;
								}
							}
							continue;
						}
						else
						{
							//TODO: obtener el nombre de la variable key del objeto sin que este instanciado. Ex: ILocation = null sacar LocationID
							foreach (var item2 in subItemInstance.GetType().GetProperties(reflectionFlags))
							{
								if (item2.PropertyType.GetNameWithoutGenericArity() == "key")
								{
									dt.Columns.Add(item2.Name);
									dt.Rows[0][item2.Name] = subItemInstance.GetType().GetProperty(item2.Name).GetValue(subItemInstance, null).GetType().GetProperty("Value").GetValue(subItemInstance.GetType().GetProperty(item2.Name).GetValue(subItemInstance, null), null);
									break;
								}
							}
						}
					}
					else if (item.PropertyType.GetNameWithoutGenericArity() == "key")
					{
						dt.Columns.Add(item.Name);
						dt.Rows[0][item.Name] = entity.GetType().GetProperty(item.Name).GetValue(entity, null).GetType().GetProperty("Value").GetValue(entity.GetType().GetProperty(item.Name).GetValue(entity, null), null);
					}
					else
					{					
						dt.Columns.Add(item.Name);
						dt.Rows[0][item.Name] = item.GetValue(entity, null);
					}
				}
			}

			return dt;
		}

		//TODO: Pasar este metodo a Base
		public static string GetNameWithoutGenericArity(this Type t)
		{
			string name = t.Name;
			int index = name.IndexOf('`');
			return index == -1 ? name : name.Substring(0, index);
		}

		//TODO: Pasar este metodo a Base y cambiar nombre por ReloadObjectFromDataBaseNeeded
		public static bool CheckKeys<T>(T entity)
		{
			object propertyValue;
			foreach (var item in entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
			{
				propertyValue = item.GetValue(entity, null);
				if (!(item.PropertyType.GetNameWithoutGenericArity() == "key")
					&& !item.PropertyType.IsArray
					&& !item.PropertyType.IsGenericType)
				{
					if (propertyValue == null)
						return true;
				}
			}

			return false;
		}

		//TODO: Pasar al base, pone los campos de la entidad de vacio a null antes de guardar en la DB
		public static void SetEmptyObjectToNull<T>(T entity)
		{
			object propertyValue;
			foreach (var item in entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
			{
				propertyValue = item.GetValue(entity, null);
				if (!(item.PropertyType.GetNameWithoutGenericArity() == "key")
					&& !Generics.IsSimpleType(item.PropertyType)
					&& item.PropertyType.IsArray
					&& item.PropertyType.IsGenericType)
				{
					if (ObjectKeyIsEmpty(propertyValue))
					{
						item.SetValue(entity, null);
					}
				}
			}
		}

		private static bool ObjectKeyIsEmpty(object obj)
		{
			object propertyValue;
			foreach (var item in obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
			{
				propertyValue = item.GetValue(obj, null);
				if (item.PropertyType.GetNameWithoutGenericArity() == "key")
				{
					try
					{
						if (propertyValue == null || propertyValue.ToString() == String.Empty || propertyValue.ToString() == "0")
						{
							return true;
						}
					}
					catch { }
				}
			}

			return false;
		}
	}

	public struct key<T>
	{
		private readonly T value;

		public key(T value)
		{
			this.value = value;
		}

		public T Value { get { return value; } }

		public static implicit operator key<T>(T s)
		{
			return new key<T>(s);
		}

		public static implicit operator T(key<T> p)
		{
			return p.Value;
		}
	}
}