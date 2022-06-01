using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostUserImplementationLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, long, int, object> _Objects;
		private readonly string PerHostObjectID = "PerHostImplementationUserLifestyleManager_" + Guid.NewGuid().ToString();

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, long, int, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			int implementationID = LifestylesMethods.GetImplementation(context);
			long userID = LifestylesMethods.GetContextKey<long>(context, "userID");

			if (!_Objects.ContainsKey(host, userID, implementationID))
			{
				_Objects[host, userID, implementationID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, userID, implementationID]);
			}

			return _Objects[host, userID, implementationID];
		}

		public override void Dispose()
		{
		}
	}
}