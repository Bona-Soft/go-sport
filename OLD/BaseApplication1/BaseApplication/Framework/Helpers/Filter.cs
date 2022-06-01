using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace MYB.BaseApplication.Framework.Helpers
{
   public class Filter : Dictionary<string, object>
   {
      /// <summary>
      ///   Returns a new filter with the values of the original Filter merged with the Filter`TFilter only if matchs by reflection with the values of TFilter.
      /// </summary>
      public Filter<TFilter> Convert<TFilter>()
      {
         return new Filter<TFilter>(this);
      }

		public static Filter<TFilter> New<TFilter>()
		{
			return new Filter<TFilter>();
		}
   }

   public class Filter<TFilter> : Dictionary<string, Type, object>
   {
      public dynamic this[string key1]
      {
         get { return get(key1); }
         set { set(value, key1); }
      }

      private dynamic get(string key1)
      {
         Type tp = GetKey2(key1);
         if (ContainsKey(key1, tp))
         {
            return base[Tuple.Create(key1, tp)];
         }
         else
         {
            return Generics.GetDefault(tp);
         }
      }

      private void set(object value, string key1)
      {
         Type objType = GetKey2(key1);
         try
         {
            if (objType.Equals(typeof(DateTime)))
            {
               DateTime d = (DateTime)Convert.ChangeType(value, objType, CultureInfo.InvariantCulture);
               base[Tuple.Create(key1, objType)] = TimeZoneInfo.ConvertTimeToUtc(d);
            }
            else
            {
               base[Tuple.Create(key1, objType)] = Convert.ChangeType(value, objType);
            }
         }
         catch
         {
            base[Tuple.Create(key1, objType)] = value;
         }
      }

      private readonly BindingFlags defaultReflecationFlags = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly;

      private Type GetKey2(string key1)
      {
         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1))
            {
               return item.Key.Item2;
            }
         }

         foreach (var item in typeof(TFilter).GetProperties(defaultReflecationFlags))
         {
            if (!item.PropertyType.IsArray
               && ((!item.PropertyType.IsGenericType || item.PropertyType.GetNameWithoutGenericArity() == "key")
                  || (item.PropertyType.IsGenericType && item.PropertyType.GetNameWithoutGenericArity() == "Nullable")))
            {
               if (item.PropertyType.GetNameWithoutGenericArity() == "key")
               {
                  if (item.Name == key1)
                     return Type.GetType(item.PropertyType.GenericTypeArguments[0].FullName);
               }
               else
               {
                  if (item.Name == key1)
                     return Type.GetType(item.PropertyType.FullName);
               }
            }
         }

         return null;
      }

		//TODO: Add ADD.

		public Filter()
		{
		
		}

		public Filter(Filter filter)
      {
         foreach (var element in filter.ToTypeDictionary<TFilter>())
            set(element.Value, element.Key.Item1);
      }

      public Filter(params object[] args)
      {
         foreach (object obj in args)
         {
            this[nameof(obj)] = obj;
         }
      }
   }
}