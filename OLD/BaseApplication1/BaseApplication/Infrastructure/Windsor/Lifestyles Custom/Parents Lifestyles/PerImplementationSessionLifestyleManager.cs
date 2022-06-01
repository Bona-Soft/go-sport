using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using System;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerImplementationSessionLifestyleManager : AbstractLifestyleManager
	{
		private readonly string PerImplementationSessionObjectID = "PerImplementationSessionLifestyleManager_" + Guid.NewGuid().ToString();

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			int implementationID = LifestylesMethods.GetImplementation(context);

			if (HttpContext.Current != null)
			{
				if (HttpContext.Current.Session[PerImplementationSessionObjectID + "_" + implementationID] == null)
				{
					HttpContext.Current.Session[PerImplementationSessionObjectID + "_" + implementationID] = base.Resolve(context, realeasePolicy);
				}

				return HttpContext.Current.Session[PerImplementationSessionObjectID + "_" + implementationID];
			}
			return "NoContext_" + implementationID;
		}

		public override void Dispose()
		{
		}
	}
}