using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostImplementationUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, int, long, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, int, long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			int implementationID = LifestylesMethods.GetImplementation(context);
			int userID = LifestylesMethods.GetContextKey<int>(context, "userID");

			if (!_Objects.ContainsKey(host, implementationID, userID))
			{
				_Objects[host, implementationID, userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, implementationID, userID]);
			}

			return _Objects[host, implementationID, userID];
		}

		public override void Dispose()
		{
		}
	}
}