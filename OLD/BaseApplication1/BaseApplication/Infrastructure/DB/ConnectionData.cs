using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration;
using System;
using System.Configuration;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.DB
{
	public class ConnectionData : IConnectionData
	{
		public string urlHost
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

		public string ConnectionString
		{
			get
			{
				string pConnectionString;

				pConnectionString = BaseConfigurationManager.GetConnectionString(urlHost);

				return pConnectionString;
			}
		}

		public string Provider
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["MainConnectionString"].ProviderName;
			}
		}

		public bool CloseConnection
		{
			get
			{
				return true;
			}
		}
	}
}