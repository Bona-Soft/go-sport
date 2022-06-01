using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerUserImplementationLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<long, int, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<long, int, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			int implementationID = LifestylesMethods.GetImplementation(context);
			long userID = LifestylesMethods.GetContextKey<long>(context, "userID");

			if (!_Objects.ContainsKey(userID, implementationID))
			{
				_Objects[userID, implementationID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[userID, implementationID]);
			}

			return _Objects[userID, implementationID];
		}

		public override void Dispose()
		{
		}
	}
}