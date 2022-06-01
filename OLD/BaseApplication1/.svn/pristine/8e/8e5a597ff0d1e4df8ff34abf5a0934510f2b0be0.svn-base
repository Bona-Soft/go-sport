using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
   public static class GenericLifestyleManager
   {
      public static TResult GetContextKey<TResult>(Castle.MicroKernel.Context.CreationContext context, string keyName) 
      {
         TResult resultValue = default(TResult);
         try
         {
            if (context.AdditionalArguments.Contains(keyName))
               return (TResult)Convert.ChangeType(context.AdditionalArguments[keyName], typeof(TResult));
         }
         catch
         {

         }
         return resultValue;
      }

      public static Type GetContextKey(Castle.MicroKernel.Context.CreationContext context, string keyName)
      {
         Type resultValue = default(Type);
         try
         {
            if (context.AdditionalArguments.Contains(keyName))
               return (Type)context.AdditionalArguments[keyName];
         }
         catch
         {

         }
         return resultValue;
      }
   }
}
