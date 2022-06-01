using System;
using System.Text;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
   public static class ObjectExt
   {
      /// <summary>
      ///   Get the property list of the object type.
      /// </summary>
      public static string PropertyList(this object obj)
      {
         var props = obj.GetType().GetProperties();
         var sb = new StringBuilder();
         foreach (var p in props)
         {
            sb.AppendLine(p.Name + ": " + p.GetValue(obj, null));
         }
         return sb.ToString();
      }

      /// <summary>
      ///   Try to convert the object to the type a string.Return String.Empty if is null or not possible. Exception controlled.
      /// </summary>
      public static string ToDefString(this object obj) => obj.ToDefString(String.Empty);

      /// <summary>
      ///   Try to convert the object to the type a string. Return defValue if is null or not possible. Exception controlled.
      /// </summary>
      public static string ToDefString(this object obj, string defValue)
      {
         try
         {
            if (obj != null)
               return obj.ToString();
         }
         catch { }
         return defValue;
      }

      /// <summary>
      ///   Try to convert the object to the type a string.Return String.Empty if is null or not possible. Exception controlled.
      /// </summary>
      public static string ToDefString<T>(this T obj) => obj.ToDefString<T>(String.Empty);

      /// <summary>
      ///   Try to convert the object to the type a string. Return defValue if is null or not possible. Exception controlled.
      /// </summary>
      public static string ToDefString<T>(this T obj, string defValue)
      {
         try
         {
            if (obj != null)
               return obj.ToString();
         }
         catch { }
         return defValue;
      }


      /// <summary>
      ///   Try to convert the object to the type T. Exception controlled.
      /// </summary>
      public static T ToDefType<T>(this object obj) => ToDefType<T>(obj, default(T));

      /// <summary>
      ///   Try to convert the object to the type T. Return default value if is not posible. Exception controlled.
      /// </summary>
      public static DateTime ToDefType<T>(this object obj, DateTime defValue) 
      {
         if(obj != null)
         {
            try
            {
               return Convert.ToDateTime(obj.ToString());
            }
            catch { }
         }
         return defValue;
      }

      /// <summary>
      ///   Try to convert the object to the type T. Return default value if is not posible. Exception controlled.
      /// </summary>
      public static T ToDefType<T>(this object obj, T defValue)
      {
         try
         {
            return (T)Convert.ChangeType(obj, typeof(T));
         }
         catch { }
         return defValue;
      }


		public static T ToDefEnum<T>(this object obj) where T : struct, IConvertible => ToDefEnum<T>(obj, default(T));
		public static T ToDefEnum<T>(this object obj, T defValue) where T : struct, IConvertible
		{
			if (!typeof(T).IsEnum || obj == null)
			{
				return defValue;
			}

			try
			{
				return (T)Enum.Parse(typeof(T), obj.ToDefType<int>().ToString(), true);
			}
			catch { }
			return defValue;
		}

		public static T ToDefEnumChar<T>(this object obj) where T : struct, IConvertible => ToDefEnumChar<T>(obj, default(T));
		public static T ToDefEnumChar<T>(this object obj, T defValue) where T : struct, IConvertible
		{
			if (!typeof(T).IsEnum || obj == null)
			{
				return defValue;
			}

			try
			{
				char c = obj.ToDefType<char>();
				return (T)Enum.Parse(typeof(T), c.ToDefType<int>().ToString(),true);
			}
			catch { }
			return defValue;
		}
	}
}