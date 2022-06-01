using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.VirtualPage
{
	public class VirtualPagesManager : IVirtualPagesManager
	{
		public VirtualPagesManager()
		{
		}

		private Dictionary<string, Tuple<HttpMethod, Func<HttpRequest, HttpResponse, bool>>> _Pages;

		public bool ExecutePage(string pageName, HttpRequest request, HttpResponse response)
		{
			CheckAndLoadPages();

			if (_Pages.ContainsKey(pageName))
			{
				_Pages[pageName].Item2(request, response);
				return true;
			}
			return false;
		}

		public bool RegisterPage(string pageName, HttpMethod httpMethod, Func<HttpRequest, HttpResponse, bool> func)
		{
			try
			{
				CheckAndLoadPages();

				_Pages.Add(pageName, new Tuple<HttpMethod, Func<HttpRequest, HttpResponse, bool>>(httpMethod, func));
				return true;
			}
			catch
			{
				return false;
			}
		}

		private string TryGetAssemblyLocation(Assembly a)
		{
			try
			{
				return a.Location;
			}
			catch
			{
				return "";
			}
		}

		private void TryLoadAssemblies(AssemblyName x, List<Assembly> loadedAssemblies)
		{
			try
			{
				loadedAssemblies.Add(AppDomain.CurrentDomain.Load(x));
			}
			catch
			{
			}
		}

		public void CheckAndLoadPages()
		{
			if (_Pages == null)
			{
				_Pages = new Dictionary<string, Tuple<HttpMethod, Func<HttpRequest, HttpResponse, bool>>>();

				var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
				loadedAssemblies
				 .SelectMany(x => x.GetReferencedAssemblies())
				 .Distinct()
				 .Where(y => loadedAssemblies.Any((a) => a.FullName == y.FullName) == false)
				 .ToList()
				 .ForEach(x => TryLoadAssemblies(x, loadedAssemblies));

				var loadedPaths = loadedAssemblies.Select(a => TryGetAssemblyLocation(a)).ToArray();

				var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
				var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
				toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

				var type = typeof(IVirtualPage);
				var types = AppDomain.CurrentDomain.GetAssemblies()
					 .SelectMany(s => s.GetTypes())
					 .Where(p => type.IsAssignableFrom(p) && p != type);

				foreach (Type t in types)
				{
					IVirtualPage virtualPage = (IVirtualPage)Activator.CreateInstance(t);
					virtualPage.Register();
				}
			}
		}

		private IEnumerable<Type> GetTypesWithInterface<T>(Assembly asm)
		{
			var it = typeof(T);
			return asm.GetLoadableTypes().Where(it.IsAssignableFrom).ToList();
		}
	}

	public static class TypeLoaderExtensions
	{
		public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
		{
			if (assembly == null) throw new ArgumentNullException("assembly");
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types.Where(t => t != null);
			}
		}
	}
}