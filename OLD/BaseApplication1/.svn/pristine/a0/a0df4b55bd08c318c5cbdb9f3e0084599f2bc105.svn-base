using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.Helpers.JTools
{
   public static class JToolExt
   {
      #region "Is null, empty or default verification"

      /// <summary>
      ///   True if the JArray is any kind of null or empty
      /// </summary>
      public static bool IsNullOrEmpty(this JArray arr)
      {
         return (arr == null)
             || (arr.Type == JTokenType.Null)
             || (arr.Type == JTokenType.Array && !arr.HasValues)
             || (arr.Type == JTokenType.String && arr.ToString() == String.Empty)
             || (arr.Type == JTokenType.Object && !arr.HasValues);
      }

      /// <summary>
      ///   True if the JObject is any kind of null or empty
      /// </summary>
      public static bool IsNullOrEmpty(this JObject obj)
      {
         return (obj == null)
             || (obj.Type == JTokenType.Null)
             || (obj.Type == JTokenType.String && obj.ToString() == String.Empty)
             || (obj.Type == JTokenType.Object && !obj.HasValues)
             || (obj.Type == JTokenType.Array && !obj.HasValues);
      }

      /// <summary>
      ///   True if the JToken is any kind of null or empty
      /// </summary>
      public static bool IsNullOrEmpty(this JToken token)
      {
         return (token == null)
             || (token.Type == JTokenType.Null)
             || (token.Type == JTokenType.String && token.ToString() == String.Empty)
             || (token.Type == JTokenType.Object && !token.HasValues)
             || (token.Type == JTokenType.Array && !token.HasValues);
      }

      /// <summary>
      ///   True if the JArray or any of his token keys are any kind of null or empty.
      ///   i.e. IsNullOrEmpty(arr,"key1","key2","keyN") is the same as arr["key1"]["key2"]["KeyN.. and will give true if arr, arr["key1"] or arr["key1"]["key2"]["keyN.. is null or empty
      /// </summary>
      public static bool IsNullOrEmpty(this JArray arr, params string[] tokenKeys)
      {
         if (tokenKeys != null && tokenKeys.Length > 0)
         {
            string firstTokenKey = tokenKeys[0];
            tokenKeys = tokenKeys.Skip(1).ToArray();
            return arr.IsNullOrEmpty() || arr[firstTokenKey].IsNullOrEmpty(tokenKeys);
         }
         return arr.IsNullOrEmpty();
      }

      /// <summary>
      ///   True if the JObject or any of his token keys are any kind of null or empty.
      ///   i.e. IsNullOrEmpty(obj,"key1","key2","keyN") is the same as obj["key1"]["key2"]["KeyN.. and will give true if obj, obj["key1"] or obj["key1"]["key2"]["keyN.. is null or empty
      /// </summary>
      public static bool IsNullOrEmpty(this JObject obj, params string[] tokenKeys)
      {
         if (tokenKeys != null && tokenKeys.Length > 0)
         {
            string firstTokenKey = tokenKeys[0];
            tokenKeys = tokenKeys.Skip(1).ToArray();
            return obj.IsNullOrEmpty() || obj[firstTokenKey].IsNullOrEmpty(tokenKeys);
         }
         return obj.IsNullOrEmpty();
      }

      /// <summary>
      ///   True if the JToken or any of his token keys are any kind of null or empty.
      ///   i.e. IsNullOrEmpty(jToken,"key1","key2","keyN") is the same as jToken["key1"]["key2"]["KeyN.. and will give true if arr, jToken["key1"] or jToken["key1"]["key2"]["keyN.. is null or empty
      /// </summary>
      public static bool IsNullOrEmpty(this JToken token, params string[] tokenKeys)
      {
         if (tokenKeys != null && tokenKeys.Length > 0)
         {
            string firstTokenKey = tokenKeys[0];
            tokenKeys = tokenKeys.Skip(1).ToArray();
            return token.IsNullOrEmpty() || token[firstTokenKey].IsNullOrEmpty(tokenKeys);
         }
         return token.IsNullOrEmpty();
      }

      /// <summary>
      ///   True if the JObject is null, empty or the default value of his type. Use IsNullOrDefault<Type> for non primitive functions, or if you want to specify the default type to compare.
      /// </summary>
      public static bool IsNullOrDefault(this JObject jobj) => IsNullOrEmpty((JToken)jobj);

      /// <summary>
      ///   True if the JToken is null, empty or the default value of his type. Use IsNullOrDefault<Type> for non primitive functions, or if you want to specify the default type to compare.
      /// </summary>
      public static bool IsNullOrDefault(this JToken token)
      {
         return IsNullOrEmpty(token)
             || (token.Type == JTokenType.Integer && token.Value<long>() == default(long))
             || (token.Type == JTokenType.Float && token.Value<float>() == default(float))
             || (token.Type == JTokenType.Boolean && !token.Value<bool>())
             || (token.Type == JTokenType.Date && token.Value<DateTime>() == default(DateTime))
             || (token.Type == JTokenType.TimeSpan && token.Value<TimeSpan>() == default(TimeSpan))
             || (token.Type == JTokenType.Bytes && token.Value<byte>() == default(byte));
      }

      /// <summary>
      ///   True if the JObject is null, empty or the default value of T type.
      /// </summary>
      public static bool IsNullOrDefault<T>(this JObject jObj) => IsNullOrDefault<T>((JToken)jObj);

      /// <summary>
      ///   True if the JToken is null, empty or the default value of T type.
      /// </summary>
      public static bool IsNullOrDefault<T>(this JToken token)
      {
         return IsNullOrEmpty(token)
             || (token.Value<T>().Equals(default(T)));
      }

      /// <summary>
      ///   True if the JArray or any of his token keys are any kind of null, empty or they own default value.
      ///   i.e. IsNullOrEmpty(arr,"key1","key2","keyN") is the same as arr["key1"]["key2"]["KeyN.. and will give true if arr, arr["key1"] or arr["key1"]["key2"]["keyN.. is null, empty or in case 0 for numbers, first system date for datetimes, etc.
      /// </summary>
      public static bool IsNullOrDefault(this JArray arr, params string[] tokenKeys)
      {
         if (tokenKeys != null && tokenKeys.Length > 0)
         {
            string firstTokenKey = tokenKeys[0];
            tokenKeys = tokenKeys.Skip(1).ToArray();
            return arr.IsNullOrDefault() || arr[firstTokenKey].IsNullOrDefault(tokenKeys);
         }
         return arr.IsNullOrDefault();
      }

      /// <summary>
      ///   True if the JObject or any of his token keys are any kind of null, empty or they own default value.
      ///   i.e. IsNullOrEmpty(obj,"key1","key2","keyN") is the same as obj["key1"]["key2"]["KeyN.. and will give true if obj, obj["key1"] or obj["key1"]["key2"]["keyN.. is null, empty or in case 0 for numbers, first system date for datetimes, etc.
      /// </summary>
      public static bool IsNullOrDefault(this JObject obj, params string[] tokenKeys)
      {
         if (tokenKeys != null && tokenKeys.Length > 0)
         {
            string firstTokenKey = tokenKeys[0];
            tokenKeys = tokenKeys.Skip(1).ToArray();
            return obj.IsNullOrDefault() || obj[firstTokenKey].IsNullOrDefault(tokenKeys);
         }
         return obj.IsNullOrDefault();
      }

      /// <summary>
      ///   True if the JToken or any of his token keys are any kind of null, empty or they own default value.
      ///   i.e. IsNullOrEmpty(token,"key1","key2","keyN") is the same as token["key1"]["key2"]["KeyN.. and will give true if token, token["key1"] or token["key1"]["key2"]["keyN.. is null, empty or in case 0 for numbers, first system date for datetimes, etc.
      /// </summary>
      public static bool IsNullOrDefault(this JToken token, params string[] tokenKeys)
      {
         if (tokenKeys != null && tokenKeys.Length > 0)
         {
            string firstTokenKey = tokenKeys[0];
            tokenKeys = tokenKeys.Skip(1).ToArray();
            return token.IsNullOrDefault() || token[firstTokenKey].IsNullOrDefault(tokenKeys);
         }
         return token.IsNullOrDefault();
      }

		public static bool IsNumber(this JObject obj)
		{
			return obj.Type == JTokenType.Integer
				|| obj.Type == JTokenType.Float;
		}

		public static bool IsNumber(this JToken token)
		{
			return token.Type == JTokenType.Integer
				|| token.Type == JTokenType.Float;
		}

		public static bool IsType<T>(this string value)
		{
			try
			{
				Type t = typeof(T);

				if (t == typeof(bool) || t == typeof(bool?))
				{
					bool iAux;
					return bool.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(byte) || t == typeof(byte?))
				{
					byte iAux;
					return byte.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(short) || t == typeof(short?))
				{
					short iAux;
					return short.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(char) || t == typeof(char?))
				{
					char iAux;
					return char.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(int) || t == typeof(int?))
				{
					int iAux;
					return int.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(long) || t == typeof(long?))
				{
					long iAux;
					return long.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(sbyte) || t == typeof(sbyte?))
				{
					sbyte iAux;
					return sbyte.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(ushort) || t == typeof(ushort?))
				{
					ushort iAux;
					return ushort.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(uint) || t == typeof(uint?))
				{
					uint iAux;
					return uint.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(ulong) || t == typeof(ulong?))
				{
					ulong iAux;
					return ulong.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(decimal) || t == typeof(decimal?))
				{
					decimal iAux;
					return decimal.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(float) || t == typeof(float?))
				{
					float iAux;
					return float.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(double) || t == typeof(double?))
				{
					double iAux;
					return double.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(DateTime) || t == typeof(DateTime?))
				{
					DateTime iAux;
					return DateTime.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(TimeSpan) || t == typeof(TimeSpan?))
				{
					TimeSpan iAux;
					return TimeSpan.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(Guid) || t == typeof(Guid?))
				{
					Guid iAux;
					return Guid.TryParse(value.ToString(), out iAux);
				}
				else if (t == typeof(Version))
				{
					Version iAux;
					return Version.TryParse(value.ToString(), out iAux);
				}
				else if (t.IsEnum)
				{
					try
					{
						object obj = Enum.Parse(typeof(T), value.ToString(), true);
						if (obj is T temp)
						{
							return true;
						}
						return false;
					}
					catch
					{
						return false;
					}
					
				}
				else
				{
					throw new Exception("Type " + typeof(T).ToString() + " is not a supported type");
				}
			}
			catch (NotSupportedException)
			{
				return false;
			}
		}

      /// <summary>
      ///   Converts the value. If null or empty return default value.
      /// </summary>
      /// <typeparam name="T">The type to convert the value to.</typeparam>
      /// <param name="jToken">A Newtonsoft.Json.Linq.JToken cast as a System.Collections.Generic.IEnumerable`1 of Newtonsoft.Json.Linq.JToken. </param>
      /// <param name="key">The key of the jToken. i.e. jtoken["key1"]</param>
      /// <param name="defaultValue"></param>
      /// <returns>A converted value.</returns>
      public static T Value<T>(this JToken jToken, string key, T defaultValue = default(T))
      {
         if (!jToken[key].IsNullOrEmpty())
         {
            return jToken.Value<T>(key);
         }
         return defaultValue;
      }

      /// <summary>
      ///   Converts the value. If null or empty return default value.
      /// </summary>
      /// <typeparam name="T">The type to convert the value to.</typeparam>
      /// <param name="jToken">A Newtonsoft.Json.Linq.JToken cast as a System.Collections.Generic.IEnumerable`1 of Newtonsoft.Json.Linq.JToken. </param>
      /// <param name="key">The key of the jToken. i.e. jtoken["key1"]</param>
      /// <param name="defaultValue">Optional: the default value if the token is null or empty</param>
      /// <returns>A converted value.</returns>
      public static T ValueDef<T>(this JToken jToken, T defaultValue = default(T))
      {
         if (!jToken.IsNullOrEmpty())
         {
            return jToken.Value<T>();
         }
         return defaultValue;
      }

      /// <summary>
      ///   Converts the value. If null or empty return default value. Is the same to call jtoken.ValueDef<T>()
      /// </summary>
      /// <typeparam name="T">The type to convert the value to.</typeparam>
      /// <param name="jToken">A Newtonsoft.Json.Linq.JToken cast as a System.Collections.Generic.IEnumerable`1 of Newtonsoft.Json.Linq.JToken. </param>
      /// <param name="key">The key of the jToken. i.e. jtoken["key1"]</param>
      /// <param name="defaultValue">Optional: the default value if the token is null or empty</param>
      /// <returns>A converted value.</returns>
      public static T Value<T>(this JToken jToken, T defaultValue)
      {
         if (!jToken.IsNullOrEmpty())
         {
            return jToken.Value<T>();
         }
         return defaultValue;
      }

      #endregion "Is null, empty or default verification"

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
      public static T ToClass<T, TID>(this JToken source, string id, Func<TID, T> factory)
         => JTool.JObjectToClass<T, TID>(source, id, factory);

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
      public static T ToClass<T, TID>(this JToken source, string id, Func<TID, T, T> factory, T facotrySecondArg)
         => JTool.JObjectToClass<T, TID>(source, id, factory, facotrySecondArg);

      /// <summary>
      ///   Execute the factory with the token[id] of the jobject source casted as TID type.
      ///   The factory method has to return an intsance of the desired class.
      ///   Then it will populate the object with the data of the jobject matching by property name/key.
      /// </summary>
      /// <typeparam name="T">Type of the return value of the factory (the instance of the class)</typeparam>
      /// <typeparam name="TID">Type of the argument of the factory, normally represent the key id of the factory and will be used as a jtoken key in the source jobject</typeparam>
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
      public static T ToClass<T, TID>(this JObject source, string id, Func<TID, T> factory)
         => JTool.JObjectToClass<T, TID>(source, id, factory);

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
      public static T ToClass<T, TID>(this JObject source, string id, Func<TID, T, T> factory, T facotrySecondArg)
         => JTool.JObjectToClass<T, TID>(source, id, factory, facotrySecondArg);

      /// <summary>
      ///    Fill an instanced object with the content of a JObject by reflection. The source properties will be readed and assing to the properties of the destination object that match the property name.
      /// </summary>
      public static T ToClass<T>(this JObject source, T destination) => JTool.JObjectToClass<T>(source, destination);

      /// <summary>
      ///   Fill an object with the content of a JObject by reflection. The source properties will be readed and assing to the properties of the destination object that match the property name.
      /// </summary>
      /// <param name="Destination">An instanced object that will be filled by the source properties values</param>
      /// <returns></returns>
      public static object ToClass(this JObject Source, object Destination) => JTool.JObjectToObject(Source, Destination);

      #endregion "JObject to Object/Class"

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
      public static JObject ToJObject(this object source, IJMapperObject mapper = null) 
			=> JTool.ObjToJObject(source, mapper);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject(this object source, JObject destinationView, BindingFlags bindingFlags, JTool.UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ObjToJObject(source, destinationView, bindingFlags, type, reflectionLevels, mapper, partialsDestinationViews);

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
      public static JObject ToJObject(this object source, JTool.UIView type, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ObjToJObject(source, type, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to a JObject. Depending on the UIView type the primitives properties, arrays or/and the objects properties will be parsed to a JObject.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject(this object source, JTool.UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ObjToJObject(source, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject(this object source, JObject destinationView, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ObjToJObject(source, destinationView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject(this object source, JObject destinationView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ObjToJObject(source, destinationView, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject(this object source, JObject destinationView, BindingFlags reflectionFlags, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ObjToJObject(source, destinationView, reflectionFlags, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject(this object source, JObject destinationView, BindingFlags reflectionFlags, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ObjToJObject(source, destinationView, reflectionFlags, reflectionLevels, mapper, partialsDestinationViews);

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
      public static JObject ToJObject<T>(this T entity, IJMapperObject mapper = null) => JTool.ClassToJObject<T>(entity, mapper);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject<T>(this T entity, JObject destinationView, BindingFlags bindingFlags, JTool.UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ClassToJObject<T>(entity, destinationView, bindingFlags, type, reflectionLevels, mapper, partialsDestinationViews);

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
      public static JObject ToJObject<T>(this T entity, JTool.UIView type, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ClassToJObject<T>(entity, type, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to a JObject. Depending on the UIView type the primitives properties, arrays or/and the objects properties will be parsed to a JObject.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject<T>(this T entity, JTool.UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ClassToJObject<T>(entity, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject<T>(this T entity, JObject destinationView, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ClassToJObject<T>(entity, destinationView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject<T>(this T entity, JObject destinationView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ClassToJObject<T>(entity, destinationView, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject<T>(this T entity, JObject destinationView, BindingFlags reflectionFlags, IJMapperObject mapper = null, params JObject[] partialsDestinationViews) 
			=> JTool.ClassToJObject<T>(entity, destinationView, reflectionFlags, mapper, partialsDestinationViews);

      /// <summary>
      ///   Given an object it will parse to the jobject destinationView provided. Will fill and return the destinationView matching by his properties names.
      ///   Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      /// </summary>
      /// <returns>A new JObject with the information of the given object</returns>
      public static JObject ToJObject<T>(this T entity, JObject destinationView, BindingFlags reflectionFlags, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ClassToJObject<T>(entity, destinationView, reflectionFlags, reflectionLevels, mapper, partialsDestinationViews);

      #endregion "Class to JObject"

      #region "System.Data to JObject"

      /// <summary>
      ///   Parse the index row number iIndex of a DataTable to a JObject
      /// </summary>
      public static JObject RowToJObject(this DataTable dtTable, int rowIndex) => JTool.DataRowToJObject(dtTable, rowIndex);

      #endregion

      #endregion "Object/Class to JObject"

      #region "Object to JArray"

      /// <summary>
      ///   Parse the rows of a DataTable to an Array
      /// </summary>
      public static JArray ToJArray(this DataTable dtTable) => JTool.DataTableToJArray(dtTable);

      #endregion "Object to JArray"

      #region "Object/Class List to JArray"

      #region "Simple convert list"

      /// <summary>
      ///   Convert an specific property in each object of the list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      ///   <para/> It takes the value of property named propertyName in each object in the list, transform to object using "<see cref="ObjToJObject(object)"/>", and then add to the jarr a new item with that propertyName keyname (jarr[i]["propertyName"])
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each specific object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> entityList, JArray jarr, string propertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T>(jarr, entityList, propertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert an specific property in each object of the list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      ///   <para/> It takes the value of property named propertyName in each object in the list, transform to object using "<see cref="ObjToJObject(object)"/>", and then add to the jarr a new item with that propertyName keyname (jarr[i]["propertyName"])
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each specific object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> entityList, string propertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T>(new JArray(), entityList, propertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert an specific property in each object of the list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      ///   <para/> It takes the value of property entityPropertyName in each object in the list, transform to object using "<see cref="ObjToJObject(object)"/>", and then add to the jarr a new item with that jarrFieldName (jarr["jarrFieldName"])
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each specific object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> entityList, JArray jarr, string jarrFieldName, string entityPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T>(jarr, entityList, jarrFieldName, entityPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert an specific property in each object of the list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      ///   <para/> It takes the value of property entityPropertyName in each object in the list, transform to object using "<see cref="ObjToJObject(object)"/>", and then add to the jarr a new item with that jarrFieldName (jarr["jarrFieldName"])
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each specific object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> entityList, string jarrFieldName, string entityPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T>(new JArray(), entityList, jarrFieldName, entityPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> source, IJMapperObject mapper = null)
          => JTool.ObjListToJArray<T>(source, mapper);

      /// <summary>
      ///   Convert a list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> source, JTool.UIView type, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray<T>(source, type, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray depending on the UIView. Short uiview will be convert only primitive properties, Full will be convert the object properties and his properties too.
      ///   <para/>For each reflection level the objects properties of the source object will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> source, JTool.UIView type, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray<T>(source, type, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray using a jobject as properties view/map.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> source, JObject destinationView, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray<T>(source, destinationView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray using a jobject as a properties view/map.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      ///   <para/>For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> source, JObject destinationView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray<T>(source, destinationView, reflectionLevels, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray depending on the jobject view.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      ///   <para/>Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> source, JObject destinationView, BindingFlags reflectionFlags, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
          => JTool.ObjListToJArray<T>(source, destinationView, reflectionFlags, mapper, partialsDestinationViews);

      /// <summary>
      ///   Convert a list to a JArray depending on the jobject view.
      ///   <para/>Will fill and return the destinationView matching by his properties names.
      ///   <para/>Select the BindingFlags to specify wich accesible levels of the source will be parse to jobject, like only properties, properties and fields, private and public properties, only public properties, etc.
      ///   <para/>For each reflection level the objects properties of the source will be parsed too, and more refleciton levels means that the objects properties of the object properties of the source will be parsed too.
      /// </summary>
      /// <returns>A new JArray with the properties names and values os each object of the original list</returns>
      public static JArray ToJArray<T>(this List<T> source, JObject destinationView, BindingFlags reflectionFlags, JTool.UIView uiView, short reflectionLevels, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T>(source, destinationView, reflectionFlags, uiView, reflectionLevels, mapper, partialsDestinationViews);

      #endregion


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
      /// <returns>The JArray provided as jarr filled with the values of the List objects of the entityList objects</returns>
      public static JArray ObjListToJArray<T, TListField>(this List<T> entityList, JArray jarr, string propertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
              => JTool.ObjListToJArray<T, TListField>(jarr, entityList, propertyName, uiView, mapper, partialsDestinationViews);

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
      public static JArray ObjListToJArray<T, TListField>(this List<T> entityList, JArray jarr, string jarrFieldName, string entityPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TListField>(jarr, entityList, jarrFieldName, entityPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, UIView)"/> but this does in two levels list. If the List has a property that is a List and these has a List too, it convert the three list to a single JArray.
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject>(this List<T> entityList, JArray jarr, string propertyName, string secondPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
              => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject>(jarr, entityList, propertyName, secondPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, string, UIView)"/> but this does in two levels list. If the List has a property that is a List and these has a List too, it convert the three list to a single JArray.
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject>(this List<T> entityList, JArray jarr,  string jarrFieldName, string secondJarrFieldName, string entityPropertyName, string secondEntityPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject>(jarr, entityList, jarrFieldName, secondJarrFieldName, entityPropertyName, secondEntityPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, String, UIView)"/> but this does in three levels list. 
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2>(this List<T> entityList, JArray destiny, string jarrFieldName, string secondJarrFieldName, string secondJarrFieldName2, string entityPropertyName, string secondEntityPropertyName, string secondEntityPropertyName2, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2>(destiny, entityList, jarrFieldName, secondJarrFieldName, secondJarrFieldName2, entityPropertyName, secondEntityPropertyName, secondEntityPropertyName2, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, String, UIView)"/> but this does in four levels list. 
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2, TChildListChildObject3>(this List<T> entityList, JArray destiny, string jarrFieldName, string secondJarrFieldName, string secondJarrFieldName2, string secondJarrFieldName3, string entityPropertyName, string secondEntityPropertyName, string secondEntityPropertyName2, string secondEntityPropertyName3, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2, TChildListChildObject3>(destiny, entityList, jarrFieldName, secondJarrFieldName, secondJarrFieldName2, secondJarrFieldName3, entityPropertyName, secondEntityPropertyName, secondEntityPropertyName2, secondEntityPropertyName3, uiView, mapper, partialsDestinationViews);

      //Without JArray as argument

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
      /// <returns>The JArray provided as jarr filled with the values of the List objects of the entityList objects</returns>
      public static JArray ObjListToJArray<T, TListField>(this List<T> entityList, string propertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
              => JTool.ObjListToJArray<T, TListField>(new JArray(), entityList, propertyName, uiView, mapper, partialsDestinationViews);

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
      public static JArray ObjListToJArray<T, TListField>(this List<T> entityList,string jarrFieldName, string entityPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TListField>(new JArray(), entityList, jarrFieldName, entityPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, UIView)"/> but this does in two levels list. If the List has a property that is a List and these has a List too, it convert the three list to a single JArray.
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject>(this List<T> entityList, string propertyName, string secondPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
              => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject>(new JArray(), entityList, propertyName, secondPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, string, UIView)"/> but this does in two levels list. If the List has a property that is a List and these has a List too, it convert the three list to a single JArray.
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject>(this List<T> entityList, string jarrFieldName, string secondJarrFieldName, string entityPropertyName, string secondEntityPropertyName, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject>(new JArray(), entityList, jarrFieldName, secondJarrFieldName, entityPropertyName, secondEntityPropertyName, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, String, UIView)"/> but this does in three levels list. 
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2>(this List<T> entityList, string jarrFieldName, string secondJarrFieldName, string secondJarrFieldName2, string entityPropertyName, string secondEntityPropertyName, string secondEntityPropertyName2, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2>(new JArray(), entityList, jarrFieldName, secondJarrFieldName, secondJarrFieldName2, entityPropertyName, secondEntityPropertyName, secondEntityPropertyName2, uiView, mapper, partialsDestinationViews);

      /// <summary>
      ///   Refer to: <see cref="ObjListToJArray{T, TListField}(JArray, List{T}, string, String, UIView)"/> but this does in four levels list. 
      /// </summary>
      public static JArray ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2, TChildListChildObject3>(this List<T> entityList, string jarrFieldName, string secondJarrFieldName, string secondJarrFieldName2, string secondJarrFieldName3, string entityPropertyName, string secondEntityPropertyName, string secondEntityPropertyName2, string secondEntityPropertyName3, JTool.UIView uiView = JTool.UIView.Full, IJMapperObject mapper = null, params JObject[] partialsDestinationViews)
         => JTool.ObjListToJArray<T, TChildListType, TChildListChildObject, TChildListChildObject2, TChildListChildObject3>(new JArray(), entityList, jarrFieldName, secondJarrFieldName, secondJarrFieldName2, secondJarrFieldName3, entityPropertyName, secondEntityPropertyName, secondEntityPropertyName2, secondEntityPropertyName3, uiView, mapper, partialsDestinationViews);

		#endregion "Advance convert list"

		#endregion

		#region "JObject and JToken helper Extension"

		public static void TryRemove(this JObject obj, string propertyName)
		{
			try
			{
				if (obj.ContainsKey(propertyName))
				{
					obj.Remove(propertyName);
				}
			}
			catch
			{

			}
		}
		#endregion
	}
}
