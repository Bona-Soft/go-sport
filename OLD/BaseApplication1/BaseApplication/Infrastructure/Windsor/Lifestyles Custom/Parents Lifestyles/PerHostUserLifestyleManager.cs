using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, long, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			long userID = LifestylesMethods.GetContextKey<long>(context, "userID");

			if (!_Objects.ContainsKey(host, userID))
			{
				_Objects[host, userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, userID]);
			}

			return _Objects[host, userID];
		}

		public override void Dispose()
		{
		}
	}
}