using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using System.Collections.Generic;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
	public class PerHostLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string auxHost = LifestylesMethods.GetDomainID();

			if (!_Objects.ContainsKey(auxHost))
			{
				_Objects[auxHost] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[auxHost]);
			}

			return _Objects[auxHost];
		}

		public override void Dispose()
		{
		}
	}
}