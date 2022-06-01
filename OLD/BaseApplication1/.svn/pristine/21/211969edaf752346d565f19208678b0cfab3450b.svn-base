using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents
{
	public class PerHostConstructorImplementationUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<string, object, int, long, object> _Objects;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<string, object, int, long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			string host = LifestylesMethods.GetDomainID();
			int implementationID = LifestylesMethods.GetImplementation(context);
			int userID = LifestylesMethods.GetContextKey<int>(context, "userID");
			object[] objects = LifestylesMethods.GetAllContextKey(context);

			if (!_Objects.ContainsKey(host, objects, implementationID, userID))
			{
				_Objects[host, objects, implementationID, userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[host, objects, implementationID, userID]);
			}

			return _Objects[host, objects, implementationID, userID];
		}

		public override void Dispose()
		{
		}
	}
}