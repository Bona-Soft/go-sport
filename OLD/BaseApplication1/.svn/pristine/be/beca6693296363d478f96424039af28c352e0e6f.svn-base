using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostConstructorLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, object, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, object, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			object[] objects = LifestylesMethods.GetAllContextKey(context);

			if (!_Objects.ContainsKey(host, objects))
			{
				_Objects[host, objects] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, objects]);
			}

			return _Objects[host, objects];
		}

		public override void Dispose()
		{
		}
	}
}