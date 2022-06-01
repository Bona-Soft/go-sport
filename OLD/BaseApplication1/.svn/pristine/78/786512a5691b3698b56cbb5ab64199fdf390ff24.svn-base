using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Security.Configuration;
using System;
using System.Web;

namespace MongoDB
{
	public class MongoConnectionData : IMongoConnectionData
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
				if (!String.IsNullOrEmpty(urlHost) && urlHost.Length >= 3 && auxHost.Substring(0, 3) == "www")
					auxHost = auxHost.Substring(4);
				return auxHost;
			}
		}

		public string ConnectionString
		{
			get
			{
				return BaseConfigurationManager.GetMongoConnectionString(urlHost);
			}
		}

		public string DataBase
		{
			get
			{
				string pConnectionString = BaseConfigurationManager.GetMongoConnectionString(urlHost);

                if (String.IsNullOrEmpty(pConnectionString))
                    return null;

				int start = pConnectionString.IndexOf("mongodb://");
				pConnectionString = pConnectionString.Substring(start, pConnectionString.Length);
				start = pConnectionString.IndexOf("/");
				if (start < 0)
					return null;
				else
					return pConnectionString.Substring(start, pConnectionString.Length);
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