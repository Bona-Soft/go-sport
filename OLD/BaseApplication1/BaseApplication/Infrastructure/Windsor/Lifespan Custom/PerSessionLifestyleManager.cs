using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Context;
using Castle.MicroKernel;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
   public class PerSessionLifestyleManager : AbstractLifestyleManager
   {
      private readonly string PerSessionObjectID = "PerSessionLifestyleManager_" + Guid.NewGuid().ToString();

      public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
      {
         if (HttpContext.Current.Session[PerSessionObjectID] == null)
         {
            // Create the actual object
            HttpContext.Current.Session[PerSessionObjectID] = base.Resolve(context, realeasePolicy);
         }

         return HttpContext.Current.Session[PerSessionObjectID];
      }

      public override void Dispose()
      {
      }
   }
}
