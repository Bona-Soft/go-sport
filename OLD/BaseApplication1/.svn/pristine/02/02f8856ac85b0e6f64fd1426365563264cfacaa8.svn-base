using System;
using System.Collections.Generic;

namespace MYB.BaseApplication.Framework.Helpers
{
   public static class GuidGenerator
   {
      private static readonly HashSet<Guid> _guids = new HashSet<Guid>();

      /// <summary>
      ///   Return a new random unique guid.
      /// </summary>
      public static Guid GetOne()
      {
         Guid result;

         lock (_guids)
            while (!_guids.Add(result = Guid.NewGuid())) ;

         return result;
      }

      /// <summary>
      ///   Return a new random guid as string
      /// </summary>
      public static string GetString()
      {
         return GuidGenerator.GetOne().ToString();
      }

      /// <summary>
      ///   Remove this guid from the HashSet. You may get this guid again in GetOne method if you utilize this guid.
      /// </summary>
      public static void Utilize(Guid guid)
      {
         lock (_guids)
            _guids.Remove(guid);
      }
   }
}