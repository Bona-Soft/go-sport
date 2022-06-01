using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerImplementationUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<int, long, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<int, long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			Dictionary<int, long, object> objects = null;
			int implementationID = LifestylesMethods.GetImplementation(context);
			long userID = LifestylesMethods.GetContextKey<long>(context, "userID");

			if (objects == null && !_Objects.ContainsKey(implementationID, userID))
			{
				_Objects[implementationID, userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[implementationID, userID]);
			}

			return _Objects[implementationID, userID];
		}

		public override void Dispose()
		{
		}
	}
}