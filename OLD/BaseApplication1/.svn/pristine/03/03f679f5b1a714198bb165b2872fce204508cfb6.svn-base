using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostConstructorUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, object, long, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, object, long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			int userID = LifestylesMethods.GetContextKey<int>(context, "userID");
			object[] objects = LifestylesMethods.GetAllContextKey(context);

			if (!_Objects.ContainsKey(host, objects, userID))
			{
				_Objects[host, objects, userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, objects, userID]);
			}

			return _Objects[host, objects, userID];
		}

		public override void Dispose()
		{
		}
	}
}