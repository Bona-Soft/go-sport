using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseFactory
	{
		public virtual TCustomInterface New<TCustomInterface, T>(T ID)
		{
			return (TCustomInterface)Activator.CreateInstance(typeof(TCustomInterface), new object[] { ID });
		}

		public virtual TCustomInterface New<TCustomInterface>()
		{
			return (TCustomInterface)Activator.CreateInstance(typeof(TCustomInterface));
		}

		public virtual TCustomInterface Resolve<TCustomInterface>()
		{
			return BaseApp.Resolve<TCustomInterface>();
		}

		public virtual TCustomInterface Resolve<TCustomInterface, T>(T id)
		{
			Dictionary<string, T> constructParameters = new Dictionary<string, T>
			{
				{ "id", id }
			};
			return BaseApp.Resolve<TCustomInterface>(constructParameters);
		}
	}

	public class BaseFactory<TClass, TInterface> where TClass : TInterface
	{
		public virtual TInterface New()
		{
			return (TInterface)Activator.CreateInstance(typeof(TClass));
		}

		public virtual TInterface New<T>(T ID)
		{
			return (TInterface)Activator.CreateInstance(typeof(TClass), new object[] { ID });
		}

      public virtual TCustomInterface New<TCustomInterface,T>(T ID)
      {
         return (TCustomInterface)Activator.CreateInstance(typeof(TCustomInterface), new object[] { ID });
      }

      public virtual TCustomInterface New<TCustomInterface>()
      {
         return (TCustomInterface)Activator.CreateInstance(typeof(TCustomInterface));
      }

      public virtual TInterface NewDef<T>(T ID, TInterface defaultValue)
		{
			if (ID == null || ID.Equals(DBNull.Value))
				return defaultValue;
			return (TInterface)Activator.CreateInstance(typeof(TClass), new object[] { ID });
		}

		public virtual TInterface NewDef<T>(object ID, TInterface defaultValue)
		{
			if (ID == null || ID.Equals(DBNull.Value))
				return defaultValue;
			return (TInterface)Activator.CreateInstance(typeof(TClass), new object[] { ID.ToDefType<T>() });
		}

		public virtual TInterface NewDefDefault<T>(T ID, TInterface defaultValue)
		{
			if (ID == null || ID.Equals(DBNull.Value) || ID.Equals(default(T)))
				return defaultValue;
			return (TInterface)Activator.CreateInstance(typeof(TClass), new object[] { ID });
		}

		public virtual TInterface Resolve()
		{
			return BaseApp.Resolve<TInterface>();
		}

		public virtual TInterface Resolve<T>(T id)
		{
			Dictionary<string, T> constructParameters = new Dictionary<string, T>
			{
				{ "id", id }
			};
			return BaseApp.Resolve<TInterface>(constructParameters);
		}

		public virtual TCustomInterface Resolve<TCustomInterface>()
		{
			return BaseApp.Resolve<TCustomInterface>();
		}

		public virtual TCustomInterface Resolve<TCustomInterface,T>(T id)
		{
			Dictionary<string, T> constructParameters = new Dictionary<string, T>
			{
				{ "id", id }
			};
			return BaseApp.Resolve<TCustomInterface>(constructParameters);
		}
	}
}