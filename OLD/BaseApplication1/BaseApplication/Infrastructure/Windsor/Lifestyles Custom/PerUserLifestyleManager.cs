using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using System.Collections.Generic;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
	public class PerUserLifestyleManager : AbstractLifestyleManager
	{
		private Dictionary<long, object> _Objects;
		//private Cache _Objects2;

		public override void Init(IComponentActivator componentActivator, IKernel kernel, ComponentModel model)
		{
			_Objects = new Dictionary<long, object>();
			base.Init(componentActivator, kernel, model);
		}

		public override object Resolve(CreationContext context, IReleasePolicy realeasePolicy)
		{
			Dictionary<long, object> userObjects = null;
			long userID = LifestylesMethods.GetContextKey<long>(context, "userID");

			if (ParentLifestyleManager.HasParent(context))
				userObjects = ParentLifestyleManager.GetObjects<long>(context);

			if (userObjects != null && !userObjects.ContainsKey(userID))
			{
				userObjects[userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(userObjects[userID]);
			}
			else if (userObjects == null && !_Objects.ContainsKey(userID))
			{
				_Objects[userID] = base.Resolve(context, realeasePolicy);
				LifestylesMethods.TryAutoInitializate(_Objects[userID]);
			}

			if (userObjects != null)
				return userObjects[userID];
			return _Objects[userID];
		}

		public override void Dispose()
		{
		}
	}
}