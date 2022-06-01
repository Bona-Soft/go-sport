using Castle.Windsor;
using System;
using System.Collections;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IContainerManager
	{
		T Resolve<T>();

		T Resolve<T>(string key);

		T Resolve<T>(IDictionary constructParameters);

		T Resolve<T>(object argumentsAsAnonymousType);

		object Resolve(string key, Type service);

		T[] ResolveAll<T>();

		T[] ResolveAll<T>(string key);

		object ResolveAll(string key, Type service);

		void Release(object obj);

		IWindsorContainer Extended { get; }
	}
}