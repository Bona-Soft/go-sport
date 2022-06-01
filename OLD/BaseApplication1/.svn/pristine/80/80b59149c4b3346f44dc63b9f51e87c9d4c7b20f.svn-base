using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MYB.BaseApplication.Framework.Helpers
{
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

	public static class Key
	{
		public static readonly BindingFlags defaultReflecationFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly;

		public static void Remove<T>(this Dictionary<string, Type, object> dic, string keyName)
		{
			dic.Remove(new Tuple<string, Type>(keyName, typeof(T)));
		}

		public static T FillEntity<T>(T entity, DataRow dr)
		{
			foreach (var item in entity.GetType().GetProperties(defaultReflecationFlags))
			{
				if (Generics.IsSimpleType(item.PropertyType))
				{
					try
					{
						item.SetValue(entity, dr[item.Name]);
					}
					catch { }
				}
			}
			return entity;
		}

		public static T FillEntity<T>(T entity, DataTable dt)
			=> FillEntity<T>(entity, dt.Rows[0]);

		public static I GetEntity<I, TFactoryParam, TID>(TID id, Func<TID, DataTable> getEntityList, Func<TFactoryParam, I> factoryNew, Func<I, DataRow, I> FillEntity, string pkString)
		{
			List<I> list = new List<I>();
			DataTable dt = getEntityList(id);

			if (dt == null || dt.Rows.Count == 0)
				return factoryNew(default(TFactoryParam));

			I entity = factoryNew(dt.Rows[0][pkString].ToDefType<TFactoryParam>());
			FillEntity(entity, dt.Rows[0]);

			return entity;
		}

		public static I GetEntity<I, TFactoryParam>(Func<DataTable> getEntityList, Func<TFactoryParam, I> factoryNew, Func<I, DataRow, I> FillEntity, string pkString)
		{
			DataTable dt = getEntityList();
			if (dt == null || dt.Rows.Count == 0)
				return factoryNew(default(TFactoryParam));

			I entity = factoryNew(dt.Rows[0][pkString].ToDefType<TFactoryParam>());
			FillEntity(entity, dt.Rows[0]);

			return entity;
		}

		/// <summary>
		///   Get a List of interface instance a get func, a factory and the way that the objet is filled.
		/// </summary>
		/// <param name="getEntityList"> A method that recive an ID and return a DataTable thats reprecente the interface I </param>
		/// <param name="factoryNew"> A method that represent a factory, recive an object and return a new instance of the param Interface(I) </param>
		/// <param name="FillEntity"> A method that recieve the instance of the interface executed by the factoryNew param, a DataRow and return the same instance of interface. The method have to fill the instance with the row data.</param>
		/// <param name="pkString"> The primary key of the DataTable loaded in the param getEntityList. It will supply the value in the DataTable to the param factoryNew </param>
		/// <returns> string: the translated string, null if not exists </returns>
		public static List<I> GetEntityList<I, TFactoryParam>(Func<DataTable> getEntityList, Func<TFactoryParam, I> factoryNew, Func<I, DataRow, I> FillEntity, string pkString)
		{
			List<I> list = new List<I>();
			DataTable dt = getEntityList();
			if (dt == null || dt.Rows.Count == 0)
				return list;

			foreach (DataRow row in dt.Rows)
			{
				I entity = factoryNew(row[pkString].ToDefType<TFactoryParam>());
				FillEntity(entity, row);
				list.Add(entity);
			}

			return list;
		}

		/// <summary>
		///   Get a List of interface instance from an id, a get func, a factory and the way that the objet is filled.
		/// </summary>
		/// <param name="id"> The ID of the method param getEntityList </param>
		/// <param name="getEntityList"> A method that recive an ID and return a DataTable thats reprecente the interface I </param>
		/// <param name="factoryNew"> A method that represent a factory, recive an object and return a new instance of the param Interface(I) </param>
		/// <param name="FillEntity"> A method that recieve the instance of the interface executed by the factoryNew param, a DataRow and return the same instance of interface. The method have to fill the instance with the row data.</param>
		/// <param name="pkString"> The primary key of the DataTable loaded in the param getEntityList. It will supply the value in the DataTable to the param factoryNew </param>
		/// <returns> string: the translated string, null if not exists </returns>
		public static List<I> GetEntityList<I, TFactoryParam, TID>(TID id, Func<TID, DataTable> getEntityList, Func<TFactoryParam, I> factoryNew, Func<I, DataRow, I> FillEntity, string pkString)
		{
			List<I> list = new List<I>();
			DataTable dt = getEntityList(id);
			if (dt == null || dt.Rows.Count == 0)
				return list;

			foreach (DataRow row in dt.Rows)
			{
				I entity = factoryNew(row[pkString].ToDefType<TFactoryParam>());
				FillEntity(entity, row);
				list.Add(entity);
			}

			return list;
		}

		public static Dictionary<string, Type, object> ToTypeDictionary<TInterface>(this Dictionary<string, object> sourceDic)
		{
			Dictionary<string, Type, object> dic = new Dictionary<string, Type, object>();

			foreach (var item in typeof(TInterface).GetProperties(defaultReflecationFlags))
			{
				if (!item.PropertyType.IsArray
					&& ((!item.PropertyType.IsGenericType || item.PropertyType.GetNameWithoutGenericArity() == "key")
						|| (item.PropertyType.IsGenericType && item.PropertyType.GetNameWithoutGenericArity() == "Nullable")))
				{
					if (item.PropertyType.GetNameWithoutGenericArity() == "key")
					{
						if (!dic.ContainsKey(item.Name, Type.GetType(item.PropertyType.GenericTypeArguments[0].FullName))
							&& sourceDic.ContainsKey(item.Name))
						{
							dic.Add(
								item.Name,
								Type.GetType(item.PropertyType.GenericTypeArguments[0].FullName),
								sourceDic[item.Name]
							);
						}
					}
					else
					{
						if (!dic.ContainsKey(item.Name, Type.GetType(item.PropertyType.FullName))
							&& sourceDic.ContainsKey(item.Name))
						{
							dic.Add(
								item.Name,
								Type.GetType(item.PropertyType.FullName),
								sourceDic[item.Name]
							);
						}
					}
				}
			}
			return dic;
		}

		public static Dictionary<string, Type, object> EntityToDictionary(object entity)
		{
			Dictionary<string, Type, object> dic = new Dictionary<string, Type, object>();

			foreach (var item in entity.GetType().GetProperties(defaultReflecationFlags))
			{
				if (!item.PropertyType.IsArray
					&& ((!item.PropertyType.IsGenericType || item.PropertyType.GetNameWithoutGenericArity() == "key")
						|| (item.PropertyType.IsGenericType && item.PropertyType.GetNameWithoutGenericArity() == "Nullable")))
				{
					if (!(item.PropertyType.GetNameWithoutGenericArity() == "key")
						&& !Generics.IsSimpleType(item.PropertyType))
					{
						object subItemInstance = item.GetValue(entity, null);

						if (subItemInstance == null)
						{
							foreach (var subItem2 in item.PropertyType.GetProperties(defaultReflecationFlags))
							{
								if (subItem2.PropertyType.GetNameWithoutGenericArity() == "key")
								{
									if (!dic.ContainsKey(subItem2.Name, Type.GetType(subItem2.PropertyType.GenericTypeArguments[0].FullName)))
									{
										dic.Add(
										subItem2.Name,
										Type.GetType(subItem2.PropertyType.GenericTypeArguments[0].FullName),
										null);
									}

									break;
								}
							}
							continue;
						}
						else
						{
							foreach (var item2 in subItemInstance.GetType().GetProperties(defaultReflecationFlags))
							{
								if (item2.PropertyType.GetNameWithoutGenericArity() == "key")
								{
									if (!dic.ContainsKey(item2.Name, Type.GetType(item2.PropertyType.GenericTypeArguments[0].FullName)))
									{
										dic.Add(
											item2.Name,
											Type.GetType(item2.PropertyType.GenericTypeArguments[0].FullName),
											subItemInstance.GetType().GetProperty(item2.Name).GetValue(subItemInstance, null).GetType().GetProperty("Value").GetValue(subItemInstance.GetType().GetProperty(item2.Name).GetValue(subItemInstance, null), null)
										);
									}
									break;
								}
							}
						}
					}
					else if (item.PropertyType.GetNameWithoutGenericArity() == "key")
					{
						if (!dic.ContainsKey(item.Name, Type.GetType(item.PropertyType.GenericTypeArguments[0].FullName)))
						{
							dic.Add(
								item.Name,
								Type.GetType(item.PropertyType.GenericTypeArguments[0].FullName),
								entity.GetType().GetProperty(item.Name).GetValue(entity, null).GetType().GetProperty("Value").GetValue(entity.GetType().GetProperty(item.Name).GetValue(entity, null), null)
							);
						}
					}
					else
					{
						if (!dic.ContainsKey(item.Name, Type.GetType(item.PropertyType.FullName)))
						{
							dic.Add(
								item.Name,
								Type.GetType(item.PropertyType.FullName),
								item.GetValue(entity, null)
							);
						}
					}
				}
			}

			return dic;
		}

		public static DataTable EntityToDataTable(object entity)
		{
			DataTable dt = new DataTable();
			dt.Rows.Add();

			foreach (var item in entity.GetType().GetProperties(defaultReflecationFlags))
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
							foreach (var subItem2 in item.PropertyType.GetProperties(defaultReflecationFlags))
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
							foreach (var item2 in subItemInstance.GetType().GetProperties(defaultReflecationFlags))
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

		public static bool ReloadObjectFromDataBaseNeeded<T>(T entity)
		{
			object propertyValue;
			foreach (var item in entity.GetType().GetProperties(defaultReflecationFlags))
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

		private static void MergeEntities<T>(T entity1, T entity2, short mergeWay = 0)
		{
			T auxEntityA;
			T auxEntityB;
			switch (mergeWay)
			{
				case 1: auxEntityA = entity2; auxEntityB = entity1; break;
				default: auxEntityA = entity1; auxEntityB = entity2; break;
			}
			foreach (var itemEntA in auxEntityA.GetType().GetProperties(defaultReflecationFlags))
			{
				object ValueA = itemEntA.GetValue(auxEntityA, null);

				if (ValueA == null)
				{
					itemEntA.SetValue(auxEntityA, auxEntityB.GetType().GetProperty(itemEntA.Name).GetValue(auxEntityB, null));
				}
			}
		}

		public static void RefillEntity<T, R>(T entity, R ID, Func<R, T> getEntityFunc)
		{
			if (ReloadObjectFromDataBaseNeeded(entity))
			{
				T persistEntity = getEntityFunc(ID);
				MergeEntities(entity, persistEntity, 0);
			}
		}

		public static void RefillEntity<T, R>(T entity, R ID, Func<T, T, T> mergeFunc, Func<R, T> getEntityFunc)
		{
			if (ReloadObjectFromDataBaseNeeded(entity))
			{
				T persistEntity = getEntityFunc(ID);
				entity = mergeFunc(entity, persistEntity);
			}
		}

		public static void SetEmptyObjectToNull<T>(T entity)
		{
			object propertyValue;
			foreach (var item in entity.GetType().GetProperties(defaultReflecationFlags))
			{
				propertyValue = item.GetValue(entity, null);
				if (!(item.PropertyType.GetNameWithoutGenericArity() == "key")
					&& !Generics.IsSimpleType(item.PropertyType)
					&& item.PropertyType.IsArray
					&& item.PropertyType.IsGenericType)
				{
					if (HasEmptyKeys(propertyValue))
					{
						item.SetValue(entity, null);
					}
				}
			}
		}

		public static bool HasEmptyKeys(object obj)
		{
			object propertyValue;
			foreach (var item in obj.GetType().GetProperties(defaultReflecationFlags))
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
}