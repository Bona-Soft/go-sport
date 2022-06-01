using Castle.MicroKernel.Context;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Security.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom
{
	public static class LifestylesMethods
	{
		public static TResult GetContextKey<TResult>(Castle.MicroKernel.Context.CreationContext context, string keyName)
		{
			TResult resultValue = default(TResult);
			try
			{
				if (context.AdditionalArguments.Contains(keyName))
					return (TResult)Convert.ChangeType(context.AdditionalArguments[keyName], typeof(TResult));
			}
			catch
			{
			}
			return resultValue;
		}

		public static Type GetContextKey(Castle.MicroKernel.Context.CreationContext context, string keyName)
		{
			Type resultValue = default(Type);
			try
			{
				if (context.AdditionalArguments.Contains(keyName))
					return (Type)context.AdditionalArguments[keyName];
			}
			catch
			{
			}
			return resultValue;
		}

		public static object[] GetAllContextKey(Castle.MicroKernel.Context.CreationContext context)
		{
			List<object> resultValue = new List<object>();
			try
			{
				foreach (object value in context.AdditionalArguments.Values)
				{
					resultValue.Add(value);
				}
			}
			catch
			{
			}
			return resultValue.ToArray();
		}

		public static void TrySetImplementationID(ref int implementationID)
		{
			if (implementationID == 0)
			{
				try
				{
					implementationID = ContainerManager.WindsorContainer.Resolve<IImplementation>().GetImplementation();
				}
				catch
				{
					implementationID = 0;
				}
			}
		}

		public static void TrySetConstructArguments(ref object[] constructArguments)
		{
			if (constructArguments != null && constructArguments.Count() > 0)
			{
				try
				{
					constructArguments = ContainerManager.WindsorContainer.Resolve<IPerConstructorController>().GetConstructor(ref constructArguments);
				}
				catch
				{
				}
			}
		}

		public static void TryAutoInitializate(dynamic obj)
		{
			try
			{
				if (obj != null && obj.GetType().GetMethod("AutoInitializate") != null)
				{
					obj.AutoInitializate();
				}
			}
			catch { }
		}

		public static int GetImplementation(CreationContext context)
		{
			int implementationID = GetContextKey<int>(context, "implementationID");
			TrySetImplementationID(ref implementationID);
			return implementationID;
		}

		public static string GetDomainID()
		{
			string host = "";

			try
			{
				if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url != null)
					host = HttpContext.Current.Request.Url.Host;
			}
			catch { }
			host = BaseConfigurationManager.GetDomainID(host);
			return host;
		}
	}
}