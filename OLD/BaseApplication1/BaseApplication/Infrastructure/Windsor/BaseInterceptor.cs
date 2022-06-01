using Castle.DynamicProxy;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.BaseApplication.Framework.LogHandler;
using System.Configuration;
using System.Reflection;

namespace MYB.BaseApplication.Infrastructure.Windsor
{
	public class BaseInterceptor : IInterceptor
	{
		private bool InterceptorEnabled = ConfigurationManager.AppSettings["Interceptor"].ToDefType(false);
		private bool BeginInterceptorEnabled = ConfigurationManager.AppSettings["Interceptor"].ToDefType(false);
		private bool AfterInterceptorEnabled = ConfigurationManager.AppSettings["Interceptor"].ToDefType(false);

		public void Intercept(IInvocation invocation)
		{
			string methodName = invocation.Method.Name;

			MemLog.Instance.C($"Intercepting method {methodName} - InterceptorEnabled={InterceptorEnabled}");

			if (InterceptorEnabled)
			{
				if (methodName == "Interceptor" || methodName == "BeginInterceptor" || methodName == "AfterInterceptor")
				{
					invocation.Proceed();
					return;
				}

				try
				{
					MethodInfo interceptorMethod = invocation.TargetType.GetMethod("Interceptor");
					object[] arguments = { invocation };
					interceptorMethod.Invoke(invocation.InvocationTarget, arguments);
				}
				catch
				{
					MemLog.Instance.W($"Intercepting method {methodName} - Interceptor does not exists");
				}

				object[] methodNameArgument = { invocation.Method.Name };

				if (BeginInterceptorEnabled)
				{
					try
					{
						MethodInfo beginInterceptorMethod = invocation.TargetType.GetMethod("BeginInterceptor");
						beginInterceptorMethod.Invoke(invocation.InvocationTarget, methodNameArgument);
					}
					catch
					{
						MemLog.Instance.W($"Intercepting method {methodName} - BeginInterceptor does not exists");
					}
				}

				invocation.Proceed();

				if (AfterInterceptorEnabled)
				{
					try
					{
						MethodInfo afterInterceptorMethod = invocation.TargetType.GetMethod("AfterInterceptor");
						afterInterceptorMethod.Invoke(invocation.InvocationTarget, methodNameArgument);
					}
					catch
					{
						MemLog.Instance.W($"Intercepting method {methodName} - AfterInterceptor does not exists");
					}
				}
			}
			else
			{
				invocation.Proceed();
			}
		}
	}
}