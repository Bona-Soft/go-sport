using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
   public static class StringExt
   {
      private static Random random = new Random();

      /// <summary>
      ///    Get a new string randomly
      /// </summary>
      /// <param name="charsToUse">Chars to be used in the randomer method</param>
      /// <returns>Returns a new string randomly</returns>
      public static string GetRandom(string charsToUse = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789") => GetRandom(charsToUse.Length, charsToUse);

      /// <summary>
      ///   Get a new string randomly
      /// </summary>
      /// <param name="length">Lenght of the new string</param>
      /// <param name="charsToUse">Chars to be used in the randomer method</param>
      /// <returns>Returns a new string randomly</returns>
      public static string GetRandom(int length, string charsToUse = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
      {
         return new string(Enumerable.Repeat(charsToUse, length)
           .Select(s => s[random.Next(s.Length)]).ToArray());
      }

      /// <summary>
      ///   Get a new string randomly
      /// </summary>
      /// <param name="charsToUse">Chars to be used in the randomer method<</param>
      /// <returns>Returns a new string randomly</returns>
      public static string ToRandom(this string charsToUse) => GetRandom(charsToUse);

      /// <summary>
      ///   Change randomnly the order of the chars
      /// </summary>
      /// <param name="charsToUse">String to be setted with the a new random string using the same oriiginal chars and lenght</param>
      public static void SetRandom(this string charsToUse)
      {
         charsToUse = GetRandom(charsToUse);
      }

      /// <summary>
      ///   Normalized a string removing special characters, most common using in urls and saving files (regex @"[#|$|%|&|@|`|/|:|;|<|=|>|?|\[|\\|\]|^|{|}|~|“|‘|+|,|\|]" to empty char)
      /// </summary>
      /// <param name="unnormalizedString">The string to be normalized</param>
      /// <returns>Return a new string without special characteres, or the same parameter if is invalid</returns>
      public static string ToNormalized(this string unnormalizedString)
      {
         if (unnormalizedString == null || unnormalizedString == String.Empty)
            return unnormalizedString;
         return Regex.Replace(unnormalizedString, @"[#|$|%|&|@|`|/|:|;|<|=|>|?|\[|\\|\]|^|{|}|~|“|‘|+|,|\|]", "");
      }
   }
}