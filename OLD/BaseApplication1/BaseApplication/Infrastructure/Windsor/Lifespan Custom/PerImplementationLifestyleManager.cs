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
   public class PerImplementationLifestyleManager : AbstractLifestyleManager
   {
      private Dictionary<string, object> m_Objects;
      private readonly string PerImplementationObjectID = "PerImplementationLifestyleManager_" + Guid.NewGuid().ToString();

      public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
      {
         string[] urlHost;

         urlHost = HttpContext.Current.Request.Url.Host.Split(new char[] { '.', '.' });

         if (m_Objects == null)
         {
            m_Objects = new Dictionary<string, object>();
         }

         if (!m_Objects.ContainsKey(urlHost[0]))
         {
            m_Objects[urlHost[0]] = base.Resolve(context, realeasePolicy);
         }

         return m_Objects[urlHost[0]];
      }

      public override void Dispose()
      {
      }
   }
}
