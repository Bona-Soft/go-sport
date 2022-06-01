using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerConstructorUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<object, long, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<object, long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			object[] objects = LifestylesMethods.GetAllContextKey(context);
			long userID = LifestylesMethods.GetContextKey<long>(context, "userID");

			if (!_Objects.ContainsKey(objects, userID))
			{
				_Objects[objects, userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[objects, userID]);
			}

			return _Objects[objects, userID];
		}

		public override void Dispose()
		{
		}
	}
}