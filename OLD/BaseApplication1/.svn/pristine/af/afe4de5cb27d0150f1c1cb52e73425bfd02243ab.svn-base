using System;
using System.Collections.Generic;
using System.Linq;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
   public static class DictonaryExt
   {
      /// <summary>
      ///  Fills and return the dictionary with the enum type with the value as int Key and the name as string Value
      /// </summary>
      /// <typeparam name="T">T must be an enum</typeparam>
      public static Dictionary<int, string> EnumNamedValues<T>(this Dictionary<int, string> dic) where T : Enum
      {
         if (dic == null)
         {
            return dic;
         }

         var values = Enum.GetValues(typeof(T));

         foreach (int item in values)
         {
            if (!dic.ContainsKey(item))
               dic.Add(item, Enum.GetName(typeof(T), item));
         }

         return dic;
      }

      /// <summary>
      ///   Rename the key to newKey. Return true if key was renamed.
      /// </summary>
      public static bool Rename<TKey, TValue>(this Dictionary<TKey,TValue> dic, TKey key, TKey newKey)
      {
         TValue _val = default;
         TKey _key = default;
         bool found = false;

         foreach (var item in dic)
         {
            if (item.Key.Equals(key))
            {
               _val = item.Value;
               _key = item.Key;
               found = true;
               break;
            }
         }

         if (found && !dic.ContainsKey(newKey))
         {
            dic.Remove(key);
            dic.Add(newKey, _val);
         }
         return found;
      }

      /// <summary>
      ///   Returns the dictionary as string. i.e. "KeyValue=ObjectValue;KeyValue2=ObjectValue2;"
      /// </summary>
      public static string ToLongString<TKey, TValue>(this Dictionary<TKey, TValue> dic, string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol, dic.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      /// <summary>
      ///   Returns the dictionary as a wrapped string. i.e. "KeyValue=ObjectValue;"
      /// </summary>
      public static string ToWrapString<TKey, TValue>(this Dictionary<TKey, TValue> dic, string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol + Environment.NewLine, dic.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary. If already exists error will be throw, use TryAddRange instead.
      /// </summary>
      public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            dic.Add(element.Key, element.Value);
      }

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary if not exists.
      /// </summary>
      public static void TryAddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
         {
            if (!dic.ContainsKey(element.Key))
            {
               dic.Add(element.Key, element.Value);
            }
         }

      }

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary, if exists replace.
      /// </summary>
      public static void ForceAddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
         {
            if (dic.ContainsKey(element.Key))
            {
               dic.Remove(element.Key);
            }
            dic.Add(element.Key, element.Value);
         }

      }

      /// <summary>
      ///   Remove the values of the dictionary dicSource to this dictionary. If not exists error will be throw, use TryRemoveRange instead.
      /// </summary>
      public static void RemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey,  TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            dic.Remove(element.Key);
      }

      /// <summary>
      ///   Remove the values of the dictionary dicSource to this dictionary if not exists.
      /// </summary>
      public static void TryRemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey,  TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
         {
            if (dic.ContainsKey(element.Key))
               dic.Remove(element.Key);
         }
		}
   }

   public interface IDictionary<TKey1,TKey2,TValue> : IDictionary<Tuple<TKey1, TKey2>, TValue> , ICloneable
   {
      #region "Basic methods"

      /// <summary>
      ///   Add a new object with two keys
      /// </summary>
       void Add(TKey1 key1, TKey2 key2, TValue value);


      /// <summary>
      ///   Remove the object that contains the 2 keys
      /// </summary>
       void Remove(TKey1 key1, TKey2 key2);

      /// <summary>
      ///   Returns true if contains the two keys"
      /// </summary>
       bool ContainsKey(TKey1 key1, TKey2 key2);

      /// <summary>
      ///   Returns the dictionary as string. i.e. "KeyValue=ObjectValue;KeyValue2=ObjectValue2;"
      /// </summary>
       string ToLongString(string middleSymbol = "=", string endSymbol = ";");

      /// <summary>
      ///   Returns the dictionary as a wrapped string. i.e. "KeyValue=ObjectValue;"
      /// </summary>
       string ToWrapString(string middleSymbol = "=", string endSymbol = ";");

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary. If already exists error will be throw, use TryAddRange instead.
      /// </summary>
       void AddRange(Dictionary<TKey1, TKey2, TValue> dicSource);

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary if not exists.
      /// </summary>
       void TryAddRange(Dictionary<TKey1, TKey2, TValue> dicSource);

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary, if exists replace.
      /// </summary>
       void ForceAddRange(Dictionary<TKey1, TKey2, TValue> dicSource);

      /// <summary>
      ///   Remove the values of the dictionary dicSource to this dictionary. If not exists error will be throw, use TryRemoveRange instead.
      /// </summary>
       void RemoveRange(Dictionary<TKey1, TKey2, TValue> dicSource);

      /// <summary>
      ///   Remove the values of the dictionary dicSource to this dictionary if not exists.
      /// </summary>
       void TryRemoveRange(Dictionary<TKey1, TKey2, TValue> dicSource);

      #endregion "Basic methods"

      #region "Rename methods"

      /// <summary>
      ///   Rename the key1 and key2 to newKey1 and newKey2. Return true if both keys were renamed.
      /// </summary>
       bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey1 newKey1Obj, TKey2 newKey2Obj);
   
      /// <summary>
      ///   Rename all the object that match only key1 to newKey1. Return true if at least one object key was renamed.
      /// </summary>
       bool Rename(TKey1 key1Obj, TKey1 newKey1Obj);
    
      /// <summary>
      ///   Rename all the object that match only key2 to newKey2. Return true if at least one object key was renamed.
      /// </summary>
       bool Rename(TKey2 key2Obj, TKey2 newKey2Obj);


      #endregion "Rename methods"

   }

   public class Dictionary<TKey1, TKey2, TValue> : Dictionary<Tuple<TKey1, TKey2>, TValue>, IDictionary<TKey1, TKey2, TValue>
   {
      public TValue this[TKey1 key1, TKey2 key2]
      {
         get { return base[Tuple.Create(key1, key2)]; }
         set { base[Tuple.Create(key1, key2)] = value; }
      }

      public Dictionary<TKey1, TKey2, TValue> Clone()
      {
         return (Dictionary<TKey1, TKey2, TValue>)this.MemberwiseClone();
      }

      object ICloneable.Clone()
      {
         return Clone();
      }

      #region "Basic methods"

      /// <summary>
      ///   Add a new object with two keys
      /// </summary>
      public void Add(TKey1 key1, TKey2 key2, TValue value)
      {
         base.Add(Tuple.Create(key1, key2), value);
      }

      /// <summary>
      ///   Remove the object that contains the 2 keys
      /// </summary>
      public void Remove(TKey1 key1, TKey2 key2)
      {
         base.Remove(Tuple.Create(key1, key2));
      }

      /// <summary>
      ///   Returns true if contains the two keys"
      /// </summary>
      public bool ContainsKey(TKey1 key1, TKey2 key2)
      {
         return base.ContainsKey(Tuple.Create(key1, key2));
      }

      /// <summary>
      ///   Returns the dictionary as string. i.e. "KeyValue=ObjectValue;KeyValue2=ObjectValue2;"
      /// </summary>
      public string ToLongString(string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol, this.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      /// <summary>
      ///   Returns the dictionary as a wrapped string. i.e. "KeyValue=ObjectValue;"
      /// </summary>
      public string ToWrapString(string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol + Environment.NewLine, this.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary. If already exists error will be throw, use TryAddRange instead.
      /// </summary>
      public void AddRange(Dictionary<TKey1, TKey2, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            this.Add(element.Key, element.Value);
      }

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary if not exists.
      /// </summary>
      public void TryAddRange(Dictionary<TKey1, TKey2, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
         {
            if (!this.ContainsKey(element.Key))
            {
               this.Add(element.Key, element.Value);
            }
         }
            
      }

      /// <summary>
      ///   Add the values of the dictionary dicSource to this dictionary, if exists replace.
      /// </summary>
      public void ForceAddRange(Dictionary<TKey1, TKey2, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
         {
            if (this.ContainsKey(element.Key))
            {
               this.Remove(element.Key);
            }
            this.Add(element.Key, element.Value);
         }

      }

      /// <summary>
      ///   Remove the values of the dictionary dicSource to this dictionary. If not exists error will be throw, use TryRemoveRange instead.
      /// </summary>
      public void RemoveRange(Dictionary<TKey1, TKey2, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            this.Remove(element.Key);
      }

      /// <summary>
      ///   Remove the values of the dictionary dicSource to this dictionary if not exists.
      /// </summary>
      public void TryRemoveRange(Dictionary<TKey1, TKey2, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
         {
            if(this.ContainsKey(element.Key))
               this.Remove(element.Key);
         }
      }

      #endregion "Basic methods"

      #region "Rename methods"

      /// <summary>
      ///   Rename the key1 and key2 to newKey1 and newKey2. Return true if both keys were renamed.
      /// </summary>
      public bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey1 newKey1Obj, TKey2 newKey2Obj)
      {
         TValue val = default(TValue);
         TKey2 key2 = default(TKey2);
         TKey1 key1 = default(TKey1);
         bool found = false;

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj) && item.Key.Item2.Equals(key2Obj))
            {
               val = item.Value;
               key1 = item.Key.Item1;
               key2 = item.Key.Item2;
               found = true;
               break;
            }
         }

         if (found && !this.ContainsKey(newKey1Obj, newKey2Obj))
         {
            this.Remove(key1, key2);
            this.Add(newKey1Obj, newKey2Obj, val);
         }
         return found;
      }

      /// <summary>
      ///   Rename all the object that match only key1 to newKey1. Return true if at least one object key was renamed.
      /// </summary>
      public bool Rename(TKey1 key1Obj, TKey1 newKey1Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TValue> auxDic = new Dictionary<TKey1, TKey2, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, item.Key.Item2, item.Value);
            }
         }

         return found;
      }

      /// <summary>
      ///   Rename all the object that match only key2 to newKey2. Return true if at least one object key was renamed.
      /// </summary>
      public bool Rename(TKey2 key2Obj, TKey2 newKey2Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TValue> auxDic = new Dictionary<TKey1, TKey2, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item2.Equals(key2Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, newKey2Obj, item.Value);
            }
         }
         return found;
      }

      #endregion "Rename methods"
   }

   public class Dictionary<TKey1, TKey2, TKey3, TValue> : Dictionary<Tuple<TKey1, TKey2, TKey3>, TValue>, IDictionary<Tuple<TKey1, TKey2, TKey3>, TValue>
   {
      public TValue this[TKey1 key1, TKey2 key2, TKey3 key3]
      {
         get { return base[Tuple.Create(key1, key2, key3)]; }
         set { base[Tuple.Create(key1, key2, key3)] = value; }
      }

      #region "Basic functions"

      public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TValue value)
      {
         base.Add(Tuple.Create(key1, key2, key3), value);
      }

      public void Remove(TKey1 key1, TKey2 key2, TKey3 key3)
      {
         base.Remove(Tuple.Create(key1, key2, key3));
      }

      public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3)
      {
         return base.ContainsKey(Tuple.Create(key1, key2, key3));
      }

      public string ToLongString(string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol, this.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      public string ToWrapString(string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol + Environment.NewLine, this.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      public void AddRange(Dictionary<TKey1, TKey2, TKey3, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            this.Add(element.Key.Item1, element.Key.Item2, element.Key.Item3, element.Value);
      }

      public void RemoveRange(Dictionary<TKey1, TKey2, TKey3, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            this.Remove(element.Key.Item1, element.Key.Item2, element.Key.Item3);
      }

      #endregion "Basic functions"

      #region "Rename complex functions"

      public bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey3 key3Obj, TKey1 newKey1Obj, TKey2 newKey2Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item2.Equals(key2Obj)
                && item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
               break;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, newKey2Obj, newKey3Obj, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey1 newKey1Obj, TKey2 newKey2Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item2.Equals(key2Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, newKey2Obj, item.Key.Item3, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey1 key1Obj, TKey3 key3Obj, TKey1 newKey1Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, item.Key.Item2, newKey3Obj, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey2 key2Obj, TKey3 key3Obj, TKey2 newKey2Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item2.Equals(key2Obj)
                && item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, newKey2Obj, newKey3Obj, item.Value);
            }
         }

         return found;
      }

      #endregion "Rename complex functions"

      #region "Rename single key methods"

      public bool Rename(TKey1 key1Obj, TKey1 newKey1Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, item.Key.Item2, item.Key.Item3, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey2 key2Obj, TKey2 newKey2Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item2.Equals(key2Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, newKey2Obj, item.Key.Item3, item.Value);
            }
         }
         return found;
      }

      public bool Rename(TKey3 key3Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, item.Key.Item2, newKey3Obj, item.Value);
            }
         }
         return found;
      }

      #endregion "Rename single key methods"
   }

   public class Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> : Dictionary<Tuple<TKey1, TKey2, TKey3, TKey4>, TValue>, IDictionary<Tuple<TKey1, TKey2, TKey3, TKey4>, TValue>
   {
      public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4]
      {
         get { return base[Tuple.Create(key1, key2, key3, key4)]; }
         set { base[Tuple.Create(key1, key2, key3, key4)] = value; }
      }

      #region "Basic functions"

      public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TValue value)
      {
         base.Add(Tuple.Create(key1, key2, key3, key4), value);
      }

      public void Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
      {
         base.Remove(Tuple.Create(key1, key2, key3, key4));
      }

      public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
      {
         return base.ContainsKey(Tuple.Create(key1, key2, key3, key4));
      }

      public string ToLongString(string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol, this.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      public string ToWrapString(string middleSymbol = "=", string endSymbol = ";")
      {
         return string.Join(endSymbol + Environment.NewLine, this.Select(x => x.Key + middleSymbol + x.Value).ToArray());
      }

      public void AddRange(Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            this.Add(element.Key, element.Value);
      }

      public void RemoveRange(Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> dicSource)
      {
         if (dicSource == null)
            throw new ArgumentNullException(nameof(dicSource));
         foreach (var element in dicSource)
            this.Remove(element.Key);
      }

      #endregion "Basic functions"

      #region "Rename complex functions"

      public bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey3 key3Obj, TKey4 key4Obj, TKey1 newKey1Obj, TKey2 newKey2Obj, TKey3 newKey3Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item2.Equals(key2Obj)
                && item.Key.Item3.Equals(key3Obj)
                && item.Key.Item4.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
               break;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, newKey2Obj, newKey3Obj, newKey4Obj, item.Value);
            }
         }

         return found;
      }

      #region "Three Keys"

      public bool Rename(TKey2 key2Obj, TKey3 key3Obj, TKey4 key4Obj, TKey2 newKey2Obj, TKey3 newKey3Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item2.Equals(key2Obj)
                && item.Key.Item3.Equals(key3Obj)
                && item.Key.Item4.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
               break;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, newKey2Obj, newKey3Obj, newKey4Obj, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey1 key1Obj, TKey3 key3Obj, TKey4 key4Obj, TKey1 newKey1Obj, TKey3 newKey3Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item3.Equals(key3Obj)
                && item.Key.Item4.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
               break;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, item.Key.Item2, newKey3Obj, newKey4Obj, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey4 key4Obj, TKey1 newKey1Obj, TKey2 newKey2Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item2.Equals(key2Obj)
                && item.Key.Item4.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
               break;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, newKey2Obj, item.Key.Item3, newKey4Obj, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey3 key3Obj, TKey1 newKey1Obj, TKey2 newKey2Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item2.Equals(key2Obj)
                && item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
               break;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, newKey2Obj, newKey3Obj, item.Key.Item4, item.Value);
            }
         }

         return found;
      }

      #endregion "Three Keys"

      #region "Two Keys"

      public bool Rename(TKey1 key1Obj, TKey2 key2Obj, TKey1 newKey1Obj, TKey2 newKey2Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item2.Equals(key2Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, newKey2Obj, item.Key.Item3, item.Key.Item4, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey1 key1Obj, TKey3 key3Obj, TKey1 newKey1Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, item.Key.Item2, newKey3Obj, item.Key.Item4, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey2 key2Obj, TKey3 key3Obj, TKey2 newKey2Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item2.Equals(key2Obj)
                && item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, newKey2Obj, newKey3Obj, item.Key.Item4, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey3 key3Obj, TKey4 key4Obj, TKey3 newKey3Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item3.Equals(key3Obj)
                && item.Key.Item4.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, item.Key.Item2, newKey3Obj, newKey4Obj, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey2 key2Obj, TKey4 key4Obj, TKey2 newKey2Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item2.Equals(key2Obj)
                && item.Key.Item4.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, newKey2Obj, item.Key.Item3, newKey4Obj, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey1 key1Obj, TKey4 key4Obj, TKey1 newKey1Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj)
                && item.Key.Item4.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, item.Key.Item2, item.Key.Item3, newKey4Obj, item.Value);
            }
         }

         return found;
      }

      #endregion "Two Keys"

      #region "One Key"

      public bool Rename(TKey1 key1Obj, TKey1 newKey1Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item1.Equals(key1Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(newKey1Obj, item.Key.Item2, item.Key.Item3, item.Key.Item4, item.Value);
            }
         }

         return found;
      }

      public bool Rename(TKey2 key2Obj, TKey2 newKey2Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item2.Equals(key2Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, newKey2Obj, item.Key.Item3, item.Key.Item4, item.Value);
            }
         }
         return found;
      }

      public bool Rename(TKey3 key3Obj, TKey3 newKey3Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item3.Equals(key3Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, item.Key.Item2, newKey3Obj, item.Key.Item4, item.Value);
            }
         }
         return found;
      }

      public bool Rename(TKey4 key4Obj, TKey4 newKey4Obj)
      {
         bool found = false;
         Dictionary<TKey1, TKey2, TKey3, TKey4, TValue> auxDic = new Dictionary<TKey1, TKey2, TKey3, TKey4, TValue>();

         foreach (var item in this)
         {
            if (item.Key.Item3.Equals(key4Obj))
            {
               auxDic.Add(item.Key, item.Value);
               found = true;
            }
         }

         if (found)
         {
            this.RemoveRange(auxDic);
            foreach (var item in auxDic)
            {
               this.Add(item.Key.Item1, item.Key.Item2, item.Key.Item3, newKey4Obj, item.Value);
            }
         }
         return found;
      }

      #endregion "One Key"

      #endregion "Rename complex functions"
   }
}