using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Elements;
using MYB.BaseApplication.Security.Configuration.Sections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MYB.BaseApplication.Security.Configuration
{
	public sealed class BaseConfigurationManager : IBaseConfigurationManager
	{
		#region " Private Methods "

		private string _PhysicalAppDir;
		private string _VirtualAppDir;
		private static IConfigConnectionSection _ConnectionSection;
		private static IConfigConnectionSection _MongoConnectionSection;

		#endregion " Private Methods "

		public string PhysicalAppDir
		{
			get
			{
				if (_PhysicalAppDir == null && HttpContext.Current != null)
				{
					_PhysicalAppDir = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + "\\";
				}
				return _PhysicalAppDir;
			}
		}

		public string VirtualAppDir
		{
			get
			{
				if (_VirtualAppDir == null)
				{
					if (HttpContext.Current != null && HttpContext.Current.Request.ApplicationPath == "/")
					{
						_VirtualAppDir = HttpContext.Current.Request.ApplicationPath;
					}

					_VirtualAppDir = HttpContext.Current.Request.ApplicationPath + "/";
				}
				return _VirtualAppDir;
			}
		}

		public string UrlHost
		{
			get
			{
				string auxHost = "";
				try
				{
					auxHost = HttpContext.Current != null ? HttpContext.Current.Request.Url.Host : "";
				}
				catch { }
				if (!String.IsNullOrEmpty(auxHost) && auxHost.Length >= 3 && auxHost.Substring(0, 3) == "www")
					auxHost = auxHost.Substring(4);
				return auxHost;
			}
		}

		public string LogDir
		{
			get
			{
				return "/Logs";
			}
		}

		public Application.CoreInterfaces.IConfigConnectionSection Connection => ConnectionSection;

		public string ConnectionString(string Host) => GetConnectionString(Host);

		public string ConnectionString() => GetConnectionString();

		public string DomainID(string urlHost) => GetDomainID(urlHost);

		#region " Static Methods "

		public static Application.CoreInterfaces.IConfigConnectionSection ConnectionSection
		{
			get
			{
				if (_ConnectionSection == null)
				{
					try
					{
						_ConnectionSection = (ConfigConnectionStringSection)ConfigurationManager.GetSection("BaseConnectionStrings");
					}
					catch
					{
						try
						{
							_ConnectionSection = (ConfigConnectionDataSection)ConfigurationManager.GetSection("BaseConnectionData");
						}
						catch
						{
						}
					}
					if (_ConnectionSection == null || _ConnectionSection.ConnectionStrings.Count == 0)
						_ConnectionSection = new ConfigMainConnectionStringSection();
				}

				return _ConnectionSection;
			}
		}

		public static IConfigConnectionSection MongoConnectionSection
		{
			get
			{
				if (_MongoConnectionSection == null)
				{
					try
					{
						_MongoConnectionSection = (ConfigMongoConnectionStringSection)ConfigurationManager.GetSection("BaseMongoConnectionStrings");
					}
					catch
					{
						try
						{
							_MongoConnectionSection = (ConfigMongoConnectionDataSection)ConfigurationManager.GetSection("BaseMongoConnectionData");
						}
						catch
						{
						}
					}
					if (_MongoConnectionSection == null || _MongoConnectionSection.ConnectionStrings.Count == 0)
						_MongoConnectionSection = new ConfigMongoMainConnectionStringSection();
				}

				return _MongoConnectionSection;
			}
		}

		public static string GetMongoConnectionString(string Host)
		{
			return MongoConnectionSection.Command.GetConnectionString(Host);
		}

		public static string GetMongoConnectionString()
		{
			return MongoConnectionSection.Command.GetConnectionString();
		}

		public static string GetConnectionString(string Host)
		{
			return ConnectionSection.Command.GetConnectionString(Host);
		}

		public static string GetConnectionString()
		{
			return ConnectionSection.Command.GetConnectionString();
		}

		public static string GetDomainID(string urlHost)
		{
			if (!String.IsNullOrEmpty(urlHost) && urlHost.Length >= 3 && urlHost.Substring(0, 3) == "www")
				urlHost = urlHost.Substring(4);

			ConfigResolveHostListSection section = (ConfigResolveHostListSection)ConfigurationManager.GetSection("ResolveHostList");
			ConfigResolveHostListCollection ResolveHostListColl = section.ResolveHostList;
			IEnumerable<ConfigResolveHostListElement> ResolveHostListElement = from ConfigResolveHostListElement cs in ResolveHostListColl
																									 where cs.Host == urlHost
																									 select cs;

			if (ResolveHostListElement.Count() > 0)
			{
				return ResolveHostListElement.First().ID;
			}

			return urlHost;
		}

		#endregion " Static Methods "
	}

	public class BaseConfigurationManager<TConfigElement> : IBaseConfigurationManager<TConfigElement> where TConfigElement : ConfigurationElement
	{
		public static string _elementName;

		public IEnumerable<TConfigElement> GetSectionElementList(string sectionName, string elementName)
		{
			_elementName = elementName;
			ConfigSection<TConfigElement> section = (ConfigSection<TConfigElement>)ConfigurationManager.GetSection(sectionName);
			ConfigCollection<TConfigElement> ConfigColl = section.ConfigColl;
			IEnumerable<TConfigElement> ConfigElements = from TConfigElement cs in ConfigColl
																		select cs;

			if (ConfigElements.Count() > 0)
			{
				return ConfigElements;
			}
			_elementName = null;
			return null;
		}
	}
}