using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using System.Collections.Generic;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
	public class PerConstructorLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<object, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<object, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
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
			else if (!_Objects.ContainsKey(constructArgument))
			{
				_Objects[constructArgument] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[constructArgument]);
			}

			return _Objects[constructArgument];
		}

		public override void Dispose()
		{
		}
	}
}