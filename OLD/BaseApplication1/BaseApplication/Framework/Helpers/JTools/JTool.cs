using MYB.BaseApplication.Framework.Helpers.TypesExt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MYB.BaseApplication.Framework.Helpers.JTools
{
   public static class JTool
   {
		public static class Mapper
		{
			public static IJMapperObject ConvertJMapperObject<T>(T source, bool hiddingMode = false)
			{
				IJMapperObject mapperObj = NewJMapperObject(hiddingMode);

				string entityName = source.GetType().GetNameWithoutGenericArity();

				foreach (var item in source.GetType().GetProperties(DefaultBindingFlags))
				{
					mapperObj.Add(entityName, item.Name);
				}

				return mapperObj;
			}

		
			public static IJMapperObject ConvertJMapperObject<TDic, TObj, TValue>(TDic dictionary, TObj obj, Func<TObj,string,string,TValue, bool> mapperFunc, bool hiddingMode = true) where TDic : IDictionary<string,IDictionary<object,string,TValue>>
			{
				IJMapperObject mapper = NewJMapperObject(hiddingMode);
				foreach(var entity in dictionary)
				{
					foreach(var prop in entity.Value)
					{
						if (mapperFunc(obj, entity.Key, prop.Key.Item2, prop.Value))
						{
							mapper.Add(entity.Key, prop.Key.Item2);
						}
					}
				}
				return mapper;
			}

			public static IJMapperObject NewJMapperObject(bool hiddingMode = true)
			{
				return (IJMapperObject)new JMapperObject(hiddingMode);
			}

		}

		/// <summary>
		///   Type of view used to in <seealso cref="JTool.ObjToJObject"/>. Short it will parse to jobject only primitive properties. Normal it will parse object too. Full or more it will parse all object and his content.
		/// </summary>
		public enum UIView
      {
         Short,
         Normal,
         Full,
         Custom1,
         Custom2,
         Custom3,
         Custom4,
         Custom5,
         Custom6,
         Custom7,
         Custom8,
         Custom9,
         Custom10
      }

      public static BindingFlags DefaultBindingFlags
      {
         get
         {
            return System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly;
         }
      }

      #region "Private Helpers Methods"

      /// <summary>
      ///   True if is primitive or enum type.
      /// </summary>
      private static bool IsSimpleType(Type type)
      {
         bool a;
         try
         {
            a = type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime);
         }
         catch
         {
            return false;
         }
         return a;
      }

      /// <summary>
      ///   Invoke a generic method.
      /// </summary>
      private static ReturnType InvokeGeneric<MethodClassType, ReturnType>(string methodName, List<Type> typeList, object[] argument)
      {
         MethodInfo method = typeof(MethodClassType).GetMethod(methodName);
         MethodInfo generic = method.MakeGenericMethod(typeList.ToArray());
         return (ReturnType)generic.Invoke(null, new object[] { argument });
      }

      /// <summary>
      ///   Get the name of the type without the generic arity (without the ´1[..)
      /// </summary>
      private static string GetNameWithoutGenericArity(this Type t)
      {
         string name = t.Name;
         int index = name.IndexOf('`');
         return index == -1 ? name : name.Substring(0, index);
      }

      #region "From Object Methods"

      private static JObject FromObject(object source, IJMapperObject mapper = null)
          => JTool.FromObject(source, mapper);

      private static JObject FromObject(object source, UIView viewType, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(source, viewType, 1, mapper, partialsDestinationViews);

      private static JObject FromObject(object source, UIView viewType, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(source, null, DefaultBindingFlags, viewType, reflectionLevels, mapper, partialsDestinationViews);

      private static JObject FromObject(object source, UIView viewType, BindingFlags reflectionFlags, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(source, null, reflectionFlags, viewType, 1, mapper, partialsDestinationViews);

      private static JObject FromObject(object source, UIView viewType, BindingFlags reflectionFlags, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(source, null, reflectionFlags, viewType, reflectionLevels, mapper, partialsDestinationViews);

      private static JObject FromObject(object source, JObject destinationView, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(source, destinationView, DefaultBindingFlags, UIView.Full, 1, mapper, partialsDestinationViews);

		

		private static JObject FromObject(object source, JObject destinationView, BindingFlags reflectionFlags, UIView viewType, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
      {
			string name;
			JToken value;
			object propertyValue;
			if (source == null)
			{
				return destinationView;
			}

			//If destination view was not provided it will do reflection to source object and viewtype, if not it will itinerate on destinationView structure.
			if (destinationView == null || destinationView.Count == 0)
			{
				destinationView = new JObject();

				string sourceName = source.GetType().Name;
				if (mapper != null && mapper.ContainsKey(sourceName))
				{
					if (mapper.HiddingMode)
					{
						destinationView = JTool.FromObject(source, UIView.Normal, reflectionFlags, 1);
						foreach (var propmapped in mapper[sourceName])
						{
							((JObject)destinationView[sourceName]).TryRemove(propmapped);
						}
					}
					else
					{
						JObject partialViewFromMapper = mapper.ToJObject();
						destinationView[sourceName] = JTool.FromObject(source, partialViewFromMapper, reflectionFlags, viewType, reflectionLevels, mapper, partialsDestinationViews);
					}
					return destinationView;
				}

				foreach (var item in source.GetType().GetProperties(reflectionFlags))
				{
					propertyValue = item.GetValue(source, null);
					destinationView[item.Name] = null;
					if (propertyValue != null)
					{
						try
						{
							//Check if is not a regular type (int, string, etc.)
							if (!IsSimpleType(item.PropertyType))
							{
								//Check if it is entity key
								if (item.PropertyType.GetNameWithoutGenericArity() == "key")
								{
									foreach (var keyValue in propertyValue.GetType().GetProperties(reflectionFlags))
									{
										destinationView[item.Name] = JValue.FromObject(keyValue.GetValue(propertyValue, null));
										break;
									}
								}
								//Check if it is a list of entities
								else if (viewType >= UIView.Full && (item.PropertyType.IsArray || item.PropertyType.IsGenericType))
								{
									JArray ja = new JArray();
									try
									{
										dynamic list = propertyValue;
										foreach (var itemAux in list)
										{
											if (reflectionLevels > 0)
											{
												ja.Add(JTool.FromObject(itemAux, UIView.Normal, reflectionFlags, Convert.ToInt16(reflectionLevels - 1)));
											}
											else
												ja.Add(JTool.FromObject(itemAux, UIView.Short, reflectionFlags, 0));
										}
									}
									catch { }
									destinationView[item.Name] = ja;
								}
								//Enter if is an object
								else if (viewType >= UIView.Normal || viewType == UIView.Full)
								{
									try
									{
										if (partialsDestinationViews != null && partialsDestinationViews.Length > 0)
										{
											foreach (JObject partView in partialsDestinationViews)
											{
												if (partView.ContainsKey(item.Name))
												{
													destinationView[item.Name] = JTool.FromObject(propertyValue, (JObject)partView.First.First, reflectionFlags, viewType, reflectionLevels);
													break;
												}
											}
										}
										else if (mapper != null && mapper.ContainsKey(item.Name))
										{	
											if (mapper.HiddingMode)
											{
												destinationView[item.Name] = JTool.FromObject(propertyValue, UIView.Normal, reflectionFlags, 1);
												foreach (var propmapped in mapper[item.Name])
												{
													((JObject)destinationView[item.Name]).TryRemove(propmapped);
												}
											}
											else
											{
												JObject partialViewFromMapper = mapper.ToJObject();
												destinationView[item.Name] = JTool.FromObject(propertyValue, partialViewFromMapper, reflectionFlags, viewType, reflectionLevels, mapper, partialsDestinationViews);
											}
										}
										else if (reflectionLevels > 0)
										{
											destinationView[item.Name] = JTool.FromObject(propertyValue, UIView.Normal, reflectionFlags, Convert.ToInt16(reflectionLevels - 1));
										}
										else
										{
											destinationView[item.Name] = JTool.FromObject(propertyValue, UIView.Short, reflectionFlags, 0);
										}
									}
									catch
									{
									}
								}
							}
							else
							{
								destinationView[item.Name] = JValue.FromObject(propertyValue);
							}
						}
						catch
						{
						}
					}
				}
			}
			else
			{
				foreach (var item in destinationView)
				{
					name = item.Key;
					value = item.Value;

					if (value.Type == JTokenType.Array)
					{
						JArray ja = new JArray();
						try
						{
							dynamic list = source.GetType().GetProperty(name).GetValue(source, null);
							foreach (var itemAux in list)
							{
								ja.Add(JTool.FromObject(itemAux, (JObject)item.Value[0]));
							}
						}
						catch { }
						destinationView[name] = ja;
					}
					else if (value.Type == JTokenType.Object)
					{
						JObject jobj = null;
						try
						{
							PropertyInfo Property = source.GetType().GetProperty(name);
							if (Property != null && Property.PropertyType.GetNameWithoutGenericArity() == "key")
							{
								propertyValue = Property.GetValue(source, null);

								foreach (var keyValue in propertyValue.GetType().GetProperties(reflectionFlags))
								{
									destinationView[name] = JValue.FromObject(keyValue.GetValue(propertyValue, null));
									break;
								}
							}
							else
							{
								jobj = JTool.FromObject(source.GetType().GetProperty(name).GetValue(source, null), (JObject)item.Value);
								destinationView[name] = jobj;
							}	
						}
						catch
						{
							destinationView[name] = jobj;
						}
					}
					else
					{
						PropertyInfo Property = source.GetType().GetProperty(name);
						if (Property != null)
						{
							propertyValue = Property.GetValue(source, null);

							//Check if it is entity key
							if (Property != null && Property.PropertyType.GetNameWithoutGenericArity() == "key")
							{
								propertyValue = Property.GetValue(source, null);

								foreach (var keyValue in propertyValue.GetType().GetProperties(reflectionFlags))
								{
									destinationView[name] = JValue.FromObject(keyValue.GetValue(propertyValue, null));
									break;
								}
							}
							else if (propertyValue != null)
							{
								destinationView[name] = JValue.FromObject(propertyValue);
							}
							else
							{
								destinationView[name] = null;
							}
						}
					}
				}
			}

			return destinationView;
		}

		#endregion "From Object Methods"

		#endregion "Private Helpers Methods"

		#region "Public Methods"

		#region "Object List to Json Object"

		#region "Simple convert list"

		/// <summary>
		///   Convert an specific property in each object of the list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
		///   <para/> It takes the value of property named propertyName in each object in the list, transform to object using "<see cref="ObjToJObject(object)"/>", and then add to the jarr a new item with that propertyName keyname (jarr[i]["propertyName"])
		/// </summary>
		/// <returns>A new JArray with the properties names and values os each specific object of the original list</returns>
		public static JArray ObjListToJArray<T>(JArray jarr, List<T> entityList, string propertyName, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => ObjListToJArray<T>(jarr, entityList, propertyName, propertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert an specific property in each object of the list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      ///   <para/> It takes the value of property entityPropertyName in each object in the list, transform to object using "<see cref="ObjToJObject(object)"/>", and then add to the jarr a new item with that jarrFieldName (jarr["jarrFieldName"])
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each specific object of the original list</returns>
      public static JArray ObjListToJArray<T>(JArray jarr, List<T> entityList, string jarrFieldName, string entityPropertyName, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
      {
         if (jarr != null && entityList != null)
         {
            int count = entityList.Count;
            for (int i = 0; i < count; i++)
            {
               object fieldValue = entityList[i].GetType().GetProperty(entityPropertyName).GetValue(entityList[i], null);
               if (fieldValue != null)
               {
                  jarr[i][jarrFieldName] = ObjToJObject((T)fieldValue, uiView, mapper, partialsDestinationViews);
               }
            }
         }

         return jarr;
      }

      /// <summary>
      ///   Convert a list to a JArray
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ObjListToJArray<T>(List<T> source, IJMapperObject mapper = null)
          => JTool.ObjListToJArray(source, UIView.Normal, mapper);

      /// <summary>
      ///   Convert a list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ObjListToJArray<T>(List<T> source, UIView type, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray(source, type, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      ///   <para/>For each reflection level the objects properties of the source object will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ObjListToJArray<T>(List<T> source, UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray(source, null, DefaultBindingFlags, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray using a jobject as properties view/map.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ObjListToJArray<T>(List<T> source, JObject destinationView, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray(source, destinationView, DefaultBindingFlags, UIView.Full, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray using a jobject as a properties view/map.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      ///   <para/>For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ObjListToJArray<T>(List<T> source, JObject destinationView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray(source, destinationView, DefaultBindingFlags, UIView.Full, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray depending on the jobject view.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      ///   <para/>Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ObjListToJArray<T>(List<T> source, JObject destinationView, BindingFlags reflectionFlags, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray(source, destinationView, reflectionFlags, UIView.Full, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray depending on the jobject view.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      ///   <para/>Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   <para/>For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ObjListToJArray<T>(List<T> source, JObject destinationView, BindingFlags reflectionFlags, UIView uiView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
      {
         JArray arr = new JArray();
         if (source == null)
            return null;
         foreach (var item in source)
         {
            arr.Add(JTool.FromObject(item, destinationView, reflectionFlags, uiView, reflectionLevels, mapper, partialsDestinationViews));
         }
         return arr;
      }

		#endregion "Simple convert list"

		#region "Advance convert list"

		/// <summary>
		///   Convert a list of object type TListField that are property of another list of object type T to a jarray.
		///   <para/> If you have a list of Foo that each Foo contains a list of Bar, use this to obtain a JArray of Foo JObjects that each json object contain a JArray of Bar JObjects.
		///   <para/> You can use <seealso cref="ObjListToJArray{T}(List{T})"/> to the first original list to get the JArray, then if the objects of the list has another list call this function with te first original list and tell to entityPropertyName argument wich is the name of the neasted list.
		/// </summary>
		/// <typeparam name="T">Type of the first list</typeparam>
		/// <typeparam name="TListField">Type of the list inside the first list</typeparam>
		/// <param name="jarr">A JArray, it can be empty or already filled.</param>
		/// <param name="entityList">The first list that contain a field with other list</param>
		/// <param name="propertyName">The name of the list that is nesated to the objects in the original list</param>
		/// <param name="partialsDestinationViews">More JObject use to partial views, each suboject that match the view will use the view</param>
		/// <returns>The JArray provided as jarr filled with the values of the List objects of the entityList objects</returns>
		public static JArray ObjListToJArray<T, TListField>(JArray jarr, List<T> entityList, string propertyName, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
              => ObjListToJArray<T, TListField>(jarr, entityList, propertyName, propertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list of object type TListField that are property of another list of object type T to a jarray.
      ///   <para/> If you have a list of Foo that each Foo contains a list of Bar, use this to obtain a JArray of Foo JObjects that each json object contain a JArray of Bar JObjects.
      ///   <para/> You can use <seealso cref="ObjListToJArray{T}(List{T})"/> to the first original list to get the JArray, then if the objects of the list has another list call this function with te first original list and tell to entityPropertyName argument wich is the name of the neasted list.
      /// </summary>
      /// <typeparam name="T">Type of the first list</typeparam>
      /// <typeparam name="TListField">Type of the list inside the first list</typeparam>
      /// <param name="jarr">A JArray, it can be empty or already filled.</param>
      /// <param name="entityList">The first list that contain a field with other list</param>
      /// <param name="jarrFieldName">Name of each JObject in the JArray as you want to call the list inside the first list values</param>
      /// <param name="entityPropertyName">The name of the list that is nesated to the objects in the original list</param>
      /// <returns>The JArray provided as jarr filled with the values of the List objects of the entityList objects</returns>
      public static JArray ObjListToJArray<T, TListField>(JArray jarr, List<T> entityList, string jarrFieldName, string entityPropertyName, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
      {
         if (jarr != null && entityList != null)
         {
            int count = entityList.Count;
            for (int i = 0; i < count; i++)
            {
               object fieldValue = entityList[i].GetType().GetProperty(entityPropertyName).GetValue(entityList[i], null);
               if (fieldValue != null)
               {
                  jarr[i][jarrFieldName] = ObjListToJArray<TListField>((List<TListField>)fieldValue, uiView, mapper, partialsDestinationViews);
               }
            }
         }

         return jarr;
      }

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, UIView)"/> but this does in two levels list. If the List has a property that is a List and these has a List too, it convert the three list to a single JArray.
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject>(JArray jarr, List<T> entityList, string propertyName, string secondPropertyName, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
              => ObjListToJArray<T, TChildListType, TChildListChildObject>(jarr, entityList, propertyName, secondPropertyName, propertyName, secondPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, string, UIView)"/> but this does in two levels list. If the List has a property that is a List and these has a List too, it convert the three list to a single JArray.
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject>(JArray jarr, List<T> entityList, string jarrFieldName, string secondJarrFieldName, string entityPropertyName, string secondEntityPropertyName, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
      {
         if (jarr != null && entityList != null)
         {
            int count = entityList.Count;
            for (int i = 0; i < count; i++)
            {
               object fieldValue = entityList[i].GetType().GetProperty(entityPropertyName).GetValue(entityList[i], null);
               if (fieldValue != null)
               {
                  List<TChildListType> fieldValueCasted = (List<TChildListType>)fieldValue;
                  jarr[i][jarrFieldName] = ObjListToJArray<TChildListType>(fieldValueCasted, uiView, mapper, partialsDestinationViews);
                  for (int k = 0; k < fieldValueCasted.Count; k++)
                  {
                     object secondFieldValue = fieldValueCasted[k].GetType().GetProperty(secondEntityPropertyName).GetValue(fieldValueCasted[k], null);
                     if (secondFieldValue != null)
                     {
                        jarr[i][jarrFieldName][k][secondJarrFieldName] = ((TChildListChildObject)secondFieldValue).ToJObject(uiView, mapper, partialsDestinationViews);
                     }
                  }
               }
            }
         }

         return jarr;
      }

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, String, UIView)"/> but this does in three levels list. 
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2>(JArray destiny, List<T> source, string jarrFieldName, string secondJarrFieldName, string secondJarrFieldName2, string entityPropertyName, string secondEntityPropertyName, string secondEntityPropertyName2, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
      {
         if (destiny != null && source != null)
         {
            int count = source.Count;
            for (int i = 0; i < count; i++)
            {
               object fieldValue = source[i].GetType().GetProperty(entityPropertyName).GetValue(source[i], null);
               if (fieldValue != null)
               {
                  List<TChildListType> fieldValueCasted = (List<TChildListType>)fieldValue;
                  destiny[i][jarrFieldName] = ObjListToJArray<TChildListType>(fieldValueCasted, uiView, mapper, partialsDestinationViews);
                  for (int k = 0; k < fieldValueCasted.Count; k++)
                  {
                     object secondFieldValue = fieldValueCasted[k].GetType().GetProperty(secondEntityPropertyName).GetValue(fieldValueCasted[k], null);
                     object secondFieldValue2 = fieldValueCasted[k].GetType().GetProperty(secondEntityPropertyName2).GetValue(fieldValueCasted[k], null);

                     if (secondFieldValue != null)
                     {
                        destiny[i][jarrFieldName][k][secondJarrFieldName] = ((TChildListChildObject)secondFieldValue).ToJObject(uiView, mapper, partialsDestinationViews);
                        destiny[i][jarrFieldName][k][secondJarrFieldName2] = ((TChildListChildObject2)secondFieldValue2).ToJObject(uiView, mapper, partialsDestinationViews);
                     }
                  }
               }
            }
         }

         return destiny;
      }

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, String, UIView)"/> but this does in four levels list. 
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2, TChildListChildObject3>(JArray destiny, List<T> source, string jarrFieldName, string secondJarrFieldName, string secondJarrFieldName2, string secondJarrFieldName3, string entityPropertyName, string secondEntityPropertyName, string secondEntityPropertyName2, string secondEntityPropertyName3, UIView uiView = UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
      {
         if (destiny != null && source != null)
         {
            int count = source.Count;
            for (int i = 0; i < count; i++)
            {
               object fieldValue = source[i].GetType().GetProperty(entityPropertyName).GetValue(source[i], null);
               if (fieldValue != null)
               {
                  List<TChildListType> fieldValueCasted = (List<TChildListType>)fieldValue;
                  destiny[i][jarrFieldName] = ObjListToJArray<TChildListType>(fieldValueCasted, uiView, mapper, partialsDestinationViews);
                  for (int k = 0; k < fieldValueCasted.Count; k++)
                  {
                     object secondFieldValue = fieldValueCasted[k].GetType().GetProperty(secondEntityPropertyName).GetValue(fieldValueCasted[k], null);
                     object secondFieldValue2 = fieldValueCasted[k].GetType().GetProperty(secondEntityPropertyName2).GetValue(fieldValueCasted[k], null);
                     object secondFieldValue3 = fieldValueCasted[k].GetType().GetProperty(secondEntityPropertyName3).GetValue(fieldValueCasted[k], null);

                     if (secondFieldValue != null)
                     {
                        destiny[i][jarrFieldName][k][secondJarrFieldName] = ((TChildListChildObject)secondFieldValue).ToJObject(uiView, mapper, partialsDestinationViews);
                        destiny[i][jarrFieldName][k][secondJarrFieldName2] = ((TChildListChildObject2)secondFieldValue2).ToJObject(uiView, mapper, partialsDestinationViews);
                        destiny[i][jarrFieldName][k][secondJarrFieldName3] = ((TChildListChildObject2)secondFieldValue3).ToJObject(uiView, mapper, partialsDestinationViews);
                     }
                  }
               }
            }
         }

         return destiny;
      }

      #endregion "Advance convert list"

      #endregion "Object List to Json Object"

      #region "Object/Class to JObject"

      #region "Object to JObject"

      /// <summary>
      ///   Given an object it will parse to a JObject. All the primitives properties, arrays and the objects properties with his properties too will be parsed to a JObject.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      /// <example><code>
      ///
      /// public class Foo
      /// {
      ///   public int FooID { get; set; }
      ///   public string Description { get; set; }
      /// }
      ///
      /// Foo foo = new Foo();
      /// foo.FooID = 1;
      /// foo.Description = "this is a test";
      ///
      /// JObject obj = ObjToJobject(foo);
      ///
      /// //obj["FooID"] gives value 1
      /// //obj["Description"] gives value "this is a test"
      ///
      /// </code></example>
      public static JObject ObjToJObject(object source, IJMapperObject mapper = null)
         => FromObject(source, UIView.Full, mapper);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ObjToJObject(object source, JObject destinationView, BindingFlags bindingFlags, UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => FromObject(source, destinationView, DefaultBindingFlags, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to a JObject. Depending on the UIView type the primitives properties, arrays or/and the objects properties with his properties too will be parsed to a JObject.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      /// <example><code>
      ///
      /// public class Foo
      /// {
      ///   public int FooID { get; set; }
      ///   public Bar bar { get; set; }
      /// }
      ///
      /// public class Bar
      /// {
      ///   public int BarID { get; set; }
      ///   public string Description { get; set; }
      /// }
      ///
      /// Bar bar = new Bar();
      /// Bar.BarID = 2;
      /// Bar.Description = "this is a test";
      ///
      /// Foo foo = new Foo();
      /// foo.FooID = 1;
      /// foo.bar = bar;
      ///
      /// JObject obj = ObjToJobject(foo,UIView.Short);
      ///
      /// //obj["FooID"] gives value 1
      /// //obj["bar"] gives value null because bar key not exists
      ///
      /// obj = ObjToJobject(foo,UIView.Full);
      ///
      /// //obj["FooID"] gives value 1
      /// //obj["bar"] gives value [jobject bar]
      /// //obj["bar"]["Description"] gives value "this is a test"
      ///
      /// </code></example>
      public static JObject ObjToJObject(object source, UIView type, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.FromObject(source, type, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to a JObject. Depending on the UIView type the primitives properties, arrays or/and the objects properties will be parsed to a JObject.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ObjToJObject(object source, UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => FromObject(source, null, DefaultBindingFlags, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ObjToJObject(object source, JObject destinationView, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => FromObject(source, destinationView, DefaultBindingFlags, UIView.Full, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ObjToJObject(object source, JObject destinationView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => FromObject(source, destinationView, DefaultBindingFlags, UIView.Full, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ObjToJObject(object source, JObject destinationView, BindingFlags reflectionFlags, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(source, destinationView, reflectionFlags, UIView.Full, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided.
      ///   Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ObjToJObject(object source, JObject destinationView, BindingFlags reflectionFlags, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(source, destinationView, reflectionFlags, UIView.Full, reflectionLevels, mapper, partialsDestinationViews);

      #endregion "Object to JObject"

      #region "Class to JObject"

      /// <summary>
      ///   Given an object it will parse to a JObject. All the primitives properties, arrays and the objects properties with his properties too will be parsed to a JObject.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      /// <example><code>
      ///
      /// public class Foo
      /// {
      ///   public int FooID { get; set; }
      ///   public string Description { get; set; }
      /// }
      ///
      /// Foo foo = new Foo();
      /// foo.FooID = 1;
      /// foo.Description = "this is a test";
      ///
      /// JObject obj = ObjToJobject(foo);
      ///
      /// //obj["FooID"] gives value 1
      /// //obj["Description"] gives value "this is a test"
      ///
      /// </code></example>
      public static JObject ClassToJObject<T>(T entity, IJMapperObject mapper = null)
         => FromObject(entity, UIView.Full, mapper);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ClassToJObject<T>(T entity, JObject destinationView, BindingFlags bindingFlags, UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => FromObject(entity, destinationView, DefaultBindingFlags, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to a JObject. Depending on the UIView type the primitives properties, arrays or/and the objects properties with his properties too will be parsed to a JObject.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      /// <example><code>
      ///
      /// public class Foo
      /// {
      ///   public int FooID { get; set; }
      ///   public Bar bar { get; set; }
      /// }
      ///
      /// public class Bar
      /// {
      ///   public int BarID { get; set; }
      ///   public string Description { get; set; }
      /// }
      ///
      /// Bar bar = new Bar();
      /// Bar.BarID = 2;
      /// Bar.Description = "this is a test";
      ///
      /// Foo foo = new Foo();
      /// foo.FooID = 1;
      /// foo.bar = bar;
      ///
      /// JObject obj = ObjToJobject(foo,UIView.Short);
      ///
      /// //obj["FooID"] gives value 1
      /// //obj["bar"] gives value null because bar key not exists
      ///
      /// obj = ObjToJobject(foo,UIView.Full);
      ///
      /// //obj["FooID"] gives value 1
      /// //obj["bar"] gives value [jobject bar]
      /// //obj["bar"]["Description"] gives value "this is a test"
      ///
      /// </code></example>
      public static JObject ClassToJObject<T>(T entity, UIView type, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.FromObject(entity, type, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to a JObject. Depending on the UIView type the primitives properties, arrays or/and the objects properties will be parsed to a JObject.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ClassToJObject<T>(T entity, UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => FromObject(entity, null, DefaultBindingFlags, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ClassToJObject<T>(T entity, JObject destinationView, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => FromObject(entity, destinationView, DefaultBindingFlags, UIView.Full, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ClassToJObject<T>(T entity, JObject destinationView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => FromObject(entity, destinationView, DefaultBindingFlags, UIView.Full, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ClassToJObject<T>(T entity, JObject destinationView, BindingFlags reflectionFlags, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(entity, destinationView, reflectionFlags, UIView.Full, 1, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ClassToJObject<T>(T entity, JObject destinationView, BindingFlags reflectionFlags, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.FromObject(entity, destinationView, reflectionFlags, UIView.Full, reflectionLevels, mapper, partialsDestinationViews);

      #endregion "Class to JObject"

      /// <summary>
      ///   Parse the index row number iIndex of a DataTable to a JObject
      /// </summary>
      public static JObject DataRowToJObject(DataTable dtTable, int rowIndex)
      {
         JObject jObject;
         DataRow drRow;

         drRow = dtTable.Rows[rowIndex];

         jObject = new JObject();
         for (int jCount = 0; jCount < dtTable.Columns.Count; jCount++)
         {
            if (drRow[jCount] != null)
            {
               if (drRow[jCount].ToString().StartsWith("{") && drRow[jCount].ToString().EndsWith("}"))
               {
                  jObject.Add(dtTable.Columns[jCount].ColumnName, JObject.Parse(drRow[jCount].ToString()));
               }
               else if (drRow[jCount].ToString().StartsWith("[") && drRow[jCount].ToString().EndsWith("]"))
               {
                  jObject.Add(dtTable.Columns[jCount].ColumnName, JArray.Parse(drRow[jCount].ToString()));
               }
               else
               {
                  jObject.Add(dtTable.Columns[jCount].ColumnName, JToken.FromObject(drRow[jCount]));
               }
            }
            else
            {
               jObject.Add(dtTable.Columns[jCount].ColumnName, JToken.FromObject(drRow[jCount]));
            }
         }

         return jObject;
      }

      #endregion "Object/Class to JObject"

      #region "JObject to Object/Class"

      /// <summary>
      ///   Execute the factory with the token[id] of the jtoken source casted as TID type.
      ///   The factory method has to return an intsance of the desired class.
      ///   Then it will populate the object with the data of the jtoken matching by property name/key.
      /// </summary>
      /// <typeparam name="T">Type of the return value of the factory (the instance of the class)</typeparam>
      /// <typeparam name="TID">Type of the argument of the factory, normally represent the key id of the factory and will be used as a jtoken key in the source jtoken</typeparam>
      /// <param name="source">The jtoken to read the properties and copy his value to the instanced object returned from the factory</param>
      /// <param name="id">The string that will be use in the source like key, and that value will be casted to TID and provided to the factory</param>
      /// <returns>Return an instance of the desired class populated with the jtoken data</returns>
      /// <example><code>
      /// public IYourClass factory(string id, IYourClass defaultClass)
      /// {
      ///   return new YourClass();
      /// }
      ///
      /// jtoken data = response.data;
      /// //data["Propertiy1"] gives value 4.
      ///
      /// IYourClass yourFilledClass = JObjectToClass&lt;IYourClass,string&gt;(data,"YourClassID",factory);
      /// //yourFilledClass.Property1 gives value 4
      ///
      /// </code></example>
      public static T JObjectToClass<T, TID>(JToken source, string id, Func<TID, T> factory)
          => JObjectToClass((JObject)source, id, factory);

      /// <summary>
      ///   Execute the factory with the token[id] casted as TID type of the jtoken source and the argument factorySecondArg.
      ///   The factory method has to return an intsance of the desired class.
      ///   Then it will populate the object with the data of the jtoken matching by property name/key.
      /// </summary>
      /// <typeparam name="T">Type of the return value of the factory (the instance of the class) and the second arguments to provide to the factory method</typeparam>
      /// <typeparam name="TID">Type of the first argument of the factory, normally represent the key id of the factory</typeparam>
      /// <param name="source">The jtoken to read the properties and copy his value to the instanced object returned from the factory</param>
      /// <param name="id">The string that will be use in the source like key, and that value will be casted to TID and provided to the factory</param>
      /// <param name="factory">A methot that given a jtoken token with key id argument it will be cast as TID type and then execute. It has to return the instance of the desired class</param>
      /// <param name="facotrySecondArg">Second argument of the factory</param>
      /// <returns>Return an instance of the desired class populated with the jtoken data</returns>
      /// <example><code>
      /// public IYourClass factory(string id, IYourClass defaultClass)
      /// {
      ///   return new YourClass();
      /// }
      ///
      /// jtoken data = response.data;
      /// //data["Propertiy1"] gives value 4.
      ///
      /// IYourClass yourFilledClass = JObjectToClass&lt;IYourClass,string&gt;(data,"YourClassID",factory,yourClassInstanceDefault);
      /// //yourFilledClass.Property1 gives value 4
      ///
      /// </code></example>
      public static T JObjectToClass<T, TID>(JToken source, string id, Func<TID, T, T> factory, T facotrySecondArg)
          => JObjectToClass((JObject)source, id, factory, facotrySecondArg);

      /// <summary>
      ///   Execute the factory with the token[id] of the jobject source casted as TID type.
      ///   The factory method has to return an intsance of the desired class.
      ///   Then it will populate the object with the data of the jobject matching by property name/key.
      /// </summary>
      /// <typeparam name="T">Type of the return value of the factory (the instance of the class)</typeparam>
      /// <typeparam name="TID">Type of the argument of the factory, normally represent the key id of the factory and will be used as a jtoken key in the source jobject</typeparam>
      /// <param name="source">The JObject to read the properties and copy his value to the instanced object returned from the factory</param>
      /// <param name="id">The string that will be use in the source like key, and that value will be casted to TID and provided to the factory</param>
      /// <returns>Return an instance of the desired class populated with the jobject data</returns>
      /// <example><code>
      /// public IYourClass factory(string id, IYourClass defaultClass)
      /// {
      ///   return new YourClass();
      /// }
      ///
      /// JObject data = response.data;
      /// //data["Propertiy1"] gives value 4.
      ///
      /// IYourClass yourFilledClass = JObjectToClass&lt;IYourClass,string&gt;(data,"YourClassID",factory);
      /// //yourFilledClass.Property1 gives value 4
      ///
      /// </code></example>
      public static T JObjectToClass<T, TID>(JObject source, string id, Func<TID, T> factory)
      {
         T entity = factory(source[id].Value<TID>());
         return JObjectToClass(source, entity);
      }

      /// <summary>
      ///   Execute the factory with the token[id] casted as TID type of the jobject source and the argument factorySecondArg.
      ///   The factory method has to return an intsance of the desired class.
      ///   Then it will populate the object with the data of the jobject matching by property name/key.
      /// </summary>
      /// <typeparam name="T">Type of the return value of the factory (the instance of the class) and the second arguments to provide to the factory method</typeparam>
      /// <typeparam name="TID">Type of the first argument of the factory, normally represent the key id of the factory</typeparam>
      /// <param name="source">The JObject to read the properties and copy his value to the instanced object returned from the factory</param>
      /// <param name="id">The string that will be use in the source like key, and that value will be casted to TID and provided to the factory</param>
      /// <param name="factory">A methot that given a jobject token with key id argument it will be cast as TID type and then execute. It has to return the instance of the desired class</param>
      /// <param name="facotrySecondArg">Second argument of the factory</param>
      /// <returns>Return an instance of the desired class populated with the jobject data</returns>
      /// <example><code>
      /// public IYourClass factory(string id, IYourClass defaultClass)
      /// {
      ///   return new YourClass();
      /// }
      ///
      /// JObject data = response.data;
      /// //data["Propertiy1"] gives value 4.
      ///
      /// IYourClass yourFilledClass = JObjectToClass&lt;IYourClass,string&gt;(data,"YourClassID",factory,yourClassInstanceDefault);
      /// //yourFilledClass.Property1 gives value 4
      ///
      /// </code></example>
      public static T JObjectToClass<T, TID>(JObject source, string id, Func<TID, T, T> factory, T facotrySecondArg)
      {
         T entity = factory(source[id].Value<TID>(), facotrySecondArg);
         return JObjectToClass(source, entity);
      }

      /// <summary>
      ///    Fill an instanced object with the content of a JObject by reflection. The source properties will be readed and assing to the properties of the destination object that match the property name.
      /// </summary>
      public static T JObjectToClass<T>(JObject source, T destination)
      {
         return (T)JObjectToObject(source, destination);
      }

      //TODO: Fijarse de pasar a private
      /// <summary>
      ///   Fill an object with the content of a JObject by reflection. The source properties will be readed and assing to the properties of the destination object that match the property name.
      /// </summary>
      /// <param name="Source">A JObject where properties and values will be taked</param>
      /// <param name="Destination">An instanced object that will be filled by the source properties values</param>
      /// <returns></returns>
      public static object JObjectToObject(JObject Source, object Destination)
      {
         string name;
         JToken value;

         if (Source == null)
         {
            return Destination;
         }

         foreach (var item in Source)
         {
            name = item.Key;
            value = item.Value;

            try
            {
               if (value.Type == JTokenType.Array)
               {
                  try
                  {
                     dynamic list = Destination.GetType().GetProperty(name, DefaultBindingFlags).GetValue(Destination, null);
                     foreach (var itemAux in value)
                     {
                        list.Add(JObjectToObject((JObject)itemAux, Destination.GetType().GetProperty(name, DefaultBindingFlags).GetValue(Destination, null)));
                     }
                  }
                  catch { }
               }
               else if (value.Type == JTokenType.Object)
               {
                  try
                  {
                     JObjectToObject((JObject)value, Destination.GetType().GetProperty(name, DefaultBindingFlags).GetValue(Destination, null));
                  }
                  catch { }
               }
               else
               {
                  JValue itemValue = (JValue)value;

                  PropertyInfo Property = Destination.GetType().GetProperty(name, DefaultBindingFlags);
                  if (Property != null)
                  {
                     if (Property.CanWrite)
                     {
                        if (!(Property.PropertyType.GetNameWithoutGenericArity() == "key"))
                        {
                           if (Property.PropertyType.Equals(typeof(int?)))
                           {
                              if (itemValue.Value != null && itemValue.Value.ToString() != String.Empty)
                              {
                                 Property.SetValue(Destination, itemValue.Value<int?>(), null);
                              }
                              else
                              {
                                 Property.SetValue(Destination, null, null);
                              }
                           }
                           else if (Property.PropertyType.Equals(typeof(float?)))
                           {
                              if (itemValue.Value != null && itemValue.Value.ToString() != String.Empty)
                              {
                                 Property.SetValue(Destination, itemValue.Value<float?>(), null);
                              }
                              else
                              {
                                 Property.SetValue(Destination, null, null);
                              }
                           }
                           else if (Property.PropertyType.Equals(typeof(decimal?)))
                           {
                              if (itemValue.Value != null && itemValue.Value.ToString() != String.Empty)
                              {
                                 Property.SetValue(Destination, itemValue.Value<decimal?>(), null);
                              }
                              else
                              {
                                 Property.SetValue(Destination, null, null);
                              }
                           }
                           else if (Property.PropertyType.Equals(typeof(DateTime?)))
                           {
                              if (itemValue.Value != null && itemValue.Value.ToString() != String.Empty)
                              {
                                 Property.SetValue(Destination, itemValue.Value<DateTime?>(), null);
                              }
                              else
                              {
                                 Property.SetValue(Destination, null, null);
                              }
                           }
                           else
                           {
                              switch (Type.GetTypeCode(Property.PropertyType))
                              {
                                 case TypeCode.Int16:
                                    Property.SetValue(Destination, itemValue.Value<Int16>(), null);
                                    break;

                                 case TypeCode.Int32:
                                    Property.SetValue(Destination, itemValue.Value<Int32>(), null);
                                    break;

                                 case TypeCode.Int64:
                                    Property.SetValue(Destination, itemValue.Value<Int64>(), null);
                                    break;

                                 case TypeCode.Decimal:
                                    Property.SetValue(Destination, itemValue.Value<Decimal>(), null);
                                    break;

                                 case TypeCode.Single:
                                    Property.SetValue(Destination, itemValue.Value<Single>(), null);
                                    break;

                                 case TypeCode.Double:
                                    Property.SetValue(Destination, itemValue.Value<Double>(), null);
                                    break;

                                 case TypeCode.DateTime:
                                    Property.SetValue(Destination, itemValue.Value<DateTime>(), null);
                                    break;

                                 default:
                                    if (itemValue.Value != null && itemValue.Value.ToString() != String.Empty)
                                    {
                                       Property.SetValue(Destination, Convert.ChangeType(itemValue.Value, Property.PropertyType), null);
                                    }
                                    break;
                              }
                           }
                        }
                     }
                  }
               }
            }
            catch { }
         }

         return Destination;
      }

      #endregion "JObject to Object/Class"

      #region "Object to JArray"

      /// <summary>
      ///   Parse the rows of a DataTable to an Array
      /// </summary>
      public static JArray DataTableToJArray(DataTable dtTable)
      {
         JArray jArray;
         JObject jObject;

         jArray = new JArray();
         for (int iCount = 0; iCount < dtTable.Rows.Count; iCount++)
         {
            jObject = DataRowToJObject(dtTable, iCount);
            jArray.Add(jObject);
         }

         return jArray;
      }

      #endregion "Object to JArray"

      #region "Parsers"

      /// <summary>
      ///   Pare a json string to JObject and populate it with the properties of a DataTAble object.
      /// </summary>
      /// <param name="jsonMap">If the jsonSource tokens names not match the jsonDest, you can use a dictonary to map.</param>
      public static JArray ParseJArray(string jsonDef, DataTable dt, Dictionary<string, string> jsonMap)
      {
         JArray jsonArr;

         jsonArr = new JArray();
         for (int iCount = 0; iCount < dt.Rows.Count; iCount++)
         {
            jsonArr.Add(ParseJObject(jsonDef, dt.Rows[iCount], jsonMap));
         }

         return jsonArr;
      }

      /// <summary>
      ///   Pare a json string to JObject and populate it with the properties of a DataRow object.
      /// </summary>
      /// <param name="jsonMap">If the jsonSource tokens names not match the jsonDest, you can use a dictonary to map.</param>
      public static JObject ParseJObject(string jsonDef, DataRow dr, Dictionary<string, string> jsonMap)
      {
         JObject jsonObj;

         jsonObj = JObject.Parse(jsonDef);
         if (dr != null)
         {
            jsonObj.Populate(dr, String.Empty, jsonMap);
         }
         return jsonObj;
      }

      #endregion "Parsers"

      #region "Populate Object"

      //TODO: Borrar si ya no se esta usando
      /// <summary>
      ///   Deprecated
      /// </summary>
      public static object ToClass<T>(object[] obj)
      {
         string name;
         JToken value;
         //JObject Source;
         //T Destination;
         JObject Source = (JObject)obj[0];
         object Destination = obj[1];

         if (Source == null)
         {
            return Destination;
         }

         foreach (var item in Source)
         {
            name = item.Key;
            value = item.Value;

            try
            {
               if (value.Type == JTokenType.Array)
               {
                  continue;
               }
               else if (value.Type == JTokenType.Object)
               {
                  continue;
               }
               else
               {
                  JValue itemValue = (JValue)value;

                  PropertyInfo Property = Destination.GetType().GetProperty(name);
                  if (Property != null)
                  {
                     if (Property.CanWrite)
                     {
                        if (Property.PropertyType.Equals(typeof(int?)))
                        {
                           if (itemValue.Value != null && itemValue.Value.ToString() != String.Empty)
                           {
                              Property.SetValue(Destination, itemValue.Value<int?>(), null);
                           }
                           else
                           {
                              Property.SetValue(Destination, null, null);
                           }
                        }
                        else if (Property.PropertyType.Equals(typeof(float?)))
                        {
                           if (itemValue.Value != null && itemValue.Value.ToString() != string.Empty)
                           {
                              Property.SetValue(Destination, itemValue.Value<float?>(), null);
                           }
                           else
                           {
                              Property.SetValue(Destination, null, null);
                           }
                        }
                        else
                        {
                           switch (Type.GetTypeCode(Property.PropertyType))
                           {
                              case TypeCode.Int16:
                                 Property.SetValue(Destination, itemValue.Value<Int16>(), null);
                                 break;

                              case TypeCode.Int32:
                                 Property.SetValue(Destination, itemValue.Value<int>(), null);
                                 break;

                              case TypeCode.Int64:
                                 Property.SetValue(Destination, itemValue.Value<Int64>(), null);
                                 break;

                              case TypeCode.Decimal:
                                 Property.SetValue(Destination, itemValue.Value<decimal>(), null);
                                 break;

                              case TypeCode.Single:
                                 Property.SetValue(Destination, itemValue.Value<float>(), null);
                                 break;

                              case TypeCode.Double:
                                 Property.SetValue(Destination, itemValue.Value<double>(), null);
                                 break;

                              default:
                                 if (itemValue.Value != null && itemValue.Value.ToString() != string.Empty)
                                 {
                                    Property.SetValue(Destination, itemValue.Value, null);
                                 }
                                 break;
                           }
                        }
                     }
                  }
               }
            }
            catch { }
         }

         return Destination;
      }

      /// <summary>
      ///   Pupulate a jsonObj with a DataRow object by property name.
      /// </summary>
      /// <param name="JsonMap">If the jsonSource tokens names not match the jsonDest, you can use a dictonary to map.</param>
      public static void Populate(this JObject jsonObj, DataRow dr, string Path, Dictionary<string, string> JsonMap)
      {
         string columnName;
         string name;
         JToken value;

         foreach (var item in jsonObj)
         {
            name = item.Key;
            value = item.Value;

            if (value.Type == JTokenType.Array)
            {
               continue;
            }
            else if (value.Type == JTokenType.Object)
            {
               ((JObject)jsonObj[name]).Populate(dr, Path + name + ".", JsonMap);
            }
            else
            {
               columnName = name;
               columnName = columnName.Replace(".", "_");

               if (JsonMap != null && JsonMap.ContainsKey(Path + name))
               {
                  columnName = JsonMap[Path + name];
               }

               if (dr.Table.Columns.Contains(columnName))
               {
                  jsonObj[name] = JToken.FromObject(dr[columnName]);
               }
            }
         }
      }

      /// <summary>
      ///   Pupulate a jsonObj with the JObject by property name.
      /// </summary>
      /// <param name="JsonMap">If the jsonSource tokens names not match the jsonDest, you can use a dictonary to map.</param>
      public static void Populate(this JObject jsonSource, JObject jsonDest, string Path, Dictionary<string, string> JsonMap)
      {
         string itemName;
         string name;
         JToken value;

         foreach (var item in jsonDest)
         {
            name = item.Key;
            value = item.Value;

            if (value.Type == JTokenType.Object)
            {
               ((JObject)jsonSource[name]).Populate(((JObject)jsonDest[name]), Path + name + ".", JsonMap);
            }
            else
            {
               itemName = name;

               if (JsonMap != null && JsonMap.ContainsKey(Path + name))
               {
                  itemName = JsonMap[Path + name];
               }

               if (jsonSource[itemName] != null)
               {
                  jsonDest[name] = jsonSource[itemName].DeepClone();
               }
            }
         }
      }

      #endregion "Populate Object"

      #region "Merge JObjects"

      /// <summary>
      ///   Type of Merge:
      ///   <para/> Inner:: Owverwrite if exists
      ///         // InnerLeft or InnerRight:: Overwrite if exists, remove if not
      ///   <para/>Left or Right:: Overwrite if exists, add if not
      ///         // OuterLeft or OuterRight:: Keep if exists, add if not
      /// </summary>
      public enum MergeType
      {
         Inner,
         Left,
         Right,
         OuterLeft,
         OuterRight,
         InnerLeft,
         InnerRight
      }

      /// <summary>
      ///   Merge into jsonDest with the values of jsonSource depending of the merge type provided.
      ///   <para/> Inner:: Owverwrite if exists
      ///         // InnerLeft or InnerRight:: Overwrite if exists, remove if not
      ///   <para/>Left or Right:: Overwrite if exists, add if not
      ///         // OuterLeft or OuterRight:: Keep if exists, add if not
      /// </summary>
      /// <param name="jsonMap">
      ///   Provide a jsonMap dictionary if jsonDest properties and jsonSource property not match.
      /// </param>
      /// <remarks>
      ///   -  Inner: Owverwrite if exists
      ///   <para> Replace existing jsonDest values with jsonSource values. </para>
      ///   <para> Properties in jsonSource that not exists in jsonDest are ignored. </para>
      ///   <para> Keep properties in jsonDest that not exists in jsonSource. </para>
      ///   <para></para>
      ///   <para>-  Left: Overwrite if exists, add if not </para>
      ///   <para> Replace existing jsonDest values with jsonSource values. </para>
      ///   <para> Properties in jsonSource that not exists in jsonDest are added. </para>
      ///   <para> Keep properties in jsonDest that not exists in jsonSource. </para>
      ///   <para></para>
      ///   <para>-  Right: Overwrite if exists, add if not </para>
      ///   <para> Replace existing jsonSource values with jsonDest values, then assing jsonSource to jsonDest. </para>
      ///   <para> Properties in jsonDest that not exists in jsonSource are added. </para>
      ///   <para> Keep properties in jsonSource that not exists in jsonDest. </para>
      ///   <para></para>
      ///   <para>-  OuterLeft: Keep if exists, add if not </para>
      ///   <para> Keep existing jsonDest property values that exists in jsonSource property. </para>
      ///   <para> Properties in jsonSource that not exists in jsonDest are added. </para>
      ///   <para> Keep properties in jsonDest that not exists in jsonSource. </para>
      ///   <para></para>
      ///   <para>-  OuterRight: Keep if exists, add if not </para>
      ///   <para> Keep existing jsonSource property values that exists in jsonDest property, then assing jsonSource to jsonDest. </para>
      ///   <para> Properties in jsonDest that not exists in jsonSource are added. </para>
      ///   <para> Keep properties in jsonSource that not exists in jsonDest. </para>
      ///   <para></para>
      ///   <para>-  InnerLeft: Overwrite if exists, remove if not </para>
      ///   <para> Replace existing jsonDest values with jsonSource values. </para>
      ///   <para> Properties in jsonSource that not exists in jsonDest are ignored. </para>
      ///   <para> Remove properties in jsonDest that not exists in jsonSource. </para>
      ///   <para></para>
      ///   <para>-  InnerRight: Overwrite if exists, remove if not </para>
      ///   <para> Replace existing jsonSource values with jsonDest values, then assing jsonSource to jsonDest. </para>
      ///   <para> Properties in jsonDest that not exists in jsonSource are ignored. </para>
      ///   <para> Remove properties in jsonSource that not exists in jsonDest. </para>
      /// </remarks>
      public static JObject Merge(JObject jsonSource, JObject jsonDest, MergeType mergeType = MergeType.Left, Dictionary<string, string> jsonMap = null)
      {
         switch (mergeType)
         {
            case MergeType.Inner:
               {
                  jsonDest = _Merger.MergeOverwrite(jsonSource, jsonDest, jsonMap);
                  break;
               }
            case MergeType.Left:
               {
                  jsonDest = _Merger.MergeUnion(jsonSource, jsonDest, jsonMap);
                  break;
               }
            case MergeType.Right:
               {
                  JObject aux = new JObject(jsonSource);
                  jsonDest = _Merger.MergeUnion(jsonDest, aux, jsonMap);
                  break;
               }
            case MergeType.OuterLeft:
               {
                  jsonDest = _Merger.MergeAdd(jsonSource, jsonDest, jsonMap);
                  break;
               }
            case MergeType.OuterRight:
               {
                  JObject aux = new JObject(jsonSource);
                  jsonDest = _Merger.MergeAdd(jsonSource, jsonDest, jsonMap);
                  break;
               }
            case MergeType.InnerLeft:
               {
                  jsonDest = _Merger.MergeOverwrite(jsonSource, jsonDest, jsonMap);
                  jsonDest = _Merger.RemoveNotInner(jsonSource, jsonDest, jsonMap);
                  break;
               }
            case MergeType.InnerRight:
               {
                  JObject aux = new JObject(jsonSource);
                  jsonDest = _Merger.MergeOverwrite(jsonDest, aux, jsonMap);
                  jsonDest = _Merger.RemoveNotInner(jsonDest, aux, jsonMap);
                  break;
               }
            default:
               {
                  break;
               }
         }
         return jsonDest;
      }

      private static class _Merger
      {
         /// <summary>
         ///   Quita los valores de jsonDest que no esten en jsonSource.
         /// </summary>
         public static JObject RemoveNotInner(JObject jsonSource, JObject jsonDest, Dictionary<string, string> JsonMap)
         {
            if (jsonSource != null)
            {
               RemoveNotInner(jsonSource, jsonDest, JsonMap, String.Empty);
            }
            return jsonDest;
         }

         public static void RemoveNotInner(JObject jsonSource, JObject jsonDest, Dictionary<string, string> JsonMap, string path)
         {
            string itemName;
            string name;
            JToken value;

            foreach (var item in jsonDest)
            {
               name = item.Key;
               value = item.Value;

               if (value.Type == JTokenType.Object)
               {
                  RemoveNotInner(((JObject)jsonSource[name]), ((JObject)jsonDest[name]), JsonMap, path + name + ".");
               }
               else
               {
                  itemName = name;

                  if (JsonMap != null && JsonMap.ContainsKey(path + name))
                  {
                     itemName = JsonMap[path + name];
                  }

                  if (jsonDest.ContainsKey(path + name) && !jsonSource.ContainsKey(itemName))
                  {
                     jsonDest.Remove(path + name);
                  }
               }
            }
         }

         ///<summary>Realiza un overwrite de los valores de jsonDest con los valores de jsonSource. Si
         ///existen propiedades adicionales en jsonSource son ignoradas</summary>
         public static JObject MergeOverwrite(JObject jsonSource, JObject jsonDest, Dictionary<string, string> JsonMap)
         {
            if (jsonSource != null)
            {
               jsonSource.Populate(jsonDest, String.Empty, JsonMap);
            }
            return jsonDest;
         }

         ///<summary>Realiza un add de los valores de jsonSource que no se encuentran en jsonDest. Las propiedades
         ///existentes en jsonDest no son modificadas</summary>
         public static JObject MergeAdd(JObject jsonSource, JObject jsonDest, Dictionary<string, string> JsonMap)
         {
            string itemName;
            string name;
            JToken value;

            if (jsonSource != null)
            {
               foreach (var item in jsonSource)
               {
                  name = item.Key;
                  value = item.Value;

                  itemName = name;

                  if (JsonMap != null && JsonMap.ContainsKey(name))
                  {
                     itemName = JsonMap[name];
                  }

                  if (jsonDest[itemName] == null)
                  {
                     jsonDest[itemName] = value.DeepClone();
                  }
               }
            }

            return jsonDest;
         }

         /// <summary>
         ///   Realiza un add de los valores de jsonSource que no se encuentran en jsonDest. Las propiedades existentes en jsonDest son reemplazadas por las de jsonSource
         /// </summary>
         /// <param name="JsonMap">If the jsonSource tokens names not match the jsonDest, you can use a dictonary to map.</param>
         /// <returns>The transformed jsonDest after merged</returns>
         public static JObject MergeUnion(JObject jsonSource, JObject jsonDest, Dictionary<string, string> JsonMap)
         {
            jsonDest = MergeOverwrite(jsonDest, jsonSource, JsonMap);
            jsonDest = MergeAdd(jsonDest, jsonSource, JsonMap);

            return jsonDest;
         }
      }

      #endregion "Merge JObjects"

      #endregion "Public Methods"
   }
}