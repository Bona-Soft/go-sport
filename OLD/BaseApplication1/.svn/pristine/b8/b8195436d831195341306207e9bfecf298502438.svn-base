using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerConstructorImplementationLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<object, int, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<object, int, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			int implementationID = LifestylesMethods.GetImplementation(context);
			object[] objects = LifestylesMethods.GetAllContextKey(context);

			if (!_Objects.ContainsKey(objects, implementationID))
			{
				_Objects[objects, implementationID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[objects, implementationID]);
			}

			return _Objects[objects, implementationID];
		}

		public override void Dispose()
		{
		}
	}
}