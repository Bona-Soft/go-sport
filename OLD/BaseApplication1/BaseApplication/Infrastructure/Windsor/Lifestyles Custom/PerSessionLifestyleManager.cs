using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using System;
using System.Collections.Generic;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
	public class PerSessionLifestyleManager : AbstractLifestyleManager
	{
		private readonly string PerSessionObjectID = "PerSessionLifestyleManager_" + Guid.NewGuid().ToString();

		private Dictionary<object, object> _SessionObjectsWithNotHttpContext;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_SessionObjectsWithNotHttpContext = new Dictionary<object, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			if(HttpContext.Current == null || HttpContext.Current.Session == null)
			{
				Dictionary<object, object> constructorObjects = null;
				object[] constructArgument = LifestylesMethods.GetAllContextKey(context);

				LifestylesMethods.TrySetConstructArguments(ref constructArgument);

				if (ParentLifestyleManager.HasParent(context))
					constructorObjects = ParentLifestyleManager.GetObjects<object>(context);

				if (constructorObjects != null && !constructorObjects.ContainsKey(constructArgument))
				{
					constructorObjects[constructArgument] = base.Resolve(context, realeasePolicy);
					LifestylesMethods.TryAutoInitializate(constructorObjects[constructArgument]);
					return constructorObjects[constructArgument];
				}
				else if (!_SessionObjectsWithNotHttpContext.ContainsKey(constructArgument))
				{
					_SessionObjectsWithNotHttpContext[constructArgument] = base.Resolve(context, realeasePolicy);
					LifestylesMethods.TryAutoInitializate(_SessionObjectsWithNotHttpContext[constructArgument]);
				}

				return _SessionObjectsWithNotHttpContext[constructArgument];
			}

			if (HttpContext.Current.Session[PerSessionObjectID] == null)
			{
				HttpContext.Current.Session[PerSessionObjectID] = base.Resolve(context, realeasePolicy);
			}	

			return HttpContext.Current.Session[PerSessionObjectID];
		}

		public override void Dispose()
		{
		}
	}
}