using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostConstructorImplementationLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, object, int, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, object, int, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			object[] objects = LifestylesMethods.GetAllContextKey(context);
			int implementationID = LifestylesMethods.GetImplementation(context);

			if (!_Objects.ContainsKey(host, objects, implementationID))
			{
				_Objects[host, objects, implementationID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, objects, implementationID]);
			}

			return _Objects[host, objects, implementationID];
		}

		public override void Dispose()
		{
		}
	}
}