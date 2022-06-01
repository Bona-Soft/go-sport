using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using System.Collections.Generic;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
	public class PerImplementationLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<int, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<int, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			Dictionary<int, object> implementationObjects = null;
			int implementationID = LifestylesMethods.GetImplementation(context);

			if (ParentLifestyleManager.HasParent(context))
				implementationObjects = ParentLifestyleManager.GetObjects<int>(context);

			if (implementationObjects != null && !implementationObjects.ContainsKey(implementationID))
			{
				implementationObjects[implementationID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(implementationObjects[implementationID]);
				return implementationObjects[implementationID];
			}
			else if (!_Objects.ContainsKey(implementationID))
			{
				_Objects[implementationID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[implementationID]);
			}

			return _Objects[implementationID];
		}

		public override void Dispose()
		{
		}
	}
}