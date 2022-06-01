using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerConstructorImplementationUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<object, int, long, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<object, int, long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			int implementationID = LifestylesMethods.GetImplementation(context);
			long userID = LifestylesMethods.GetContextKey<long>(context, "userID");
			object[] objects = LifestylesMethods.GetAllContextKey(context);

			if (!_Objects.ContainsKey(objects, implementationID, userID))
			{
				_Objects[objects, implementationID, userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[objects, implementationID, userID]);
			}

			return _Objects[objects, implementationID, userID];
		}

		public override void Dispose()
		{
		}
	}
}