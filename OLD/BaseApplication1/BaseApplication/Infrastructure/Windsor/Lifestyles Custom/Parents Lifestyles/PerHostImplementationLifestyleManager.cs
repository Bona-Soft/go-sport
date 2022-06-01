using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostImplementationLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, int, object> _Objects;
		private readonly string PerHostObjectID = "PerHostImplementationUserLifestyleManager_" + Guid.NewGuid().ToString();

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, int, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			int implementationID = LifestylesMethods.GetImplementation(context);

			if (!_Objects.ContainsKey(host, implementationID))
			{
				_Objects[host, implementationID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, implementationID]);
			}

			return _Objects[host, implementationID];
		}

		public override void Dispose()
		{
		}
	}
}