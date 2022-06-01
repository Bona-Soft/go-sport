using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MYB.BaseApplication.Security.Configuration.Commands
{
	public class BaseConfigMongoConnectionDataCommands : IBaseConfigurationBehavior
	{
		public string GetConnectionString()
		{
			return GetConnectionString("");
		}

		public string GetConnectionString(string Host)
		{
			IConfigConnectionCollections connDataColl = BaseConfigurationManager.ConnectionSection.ConnectionStrings;
			IEnumerable<ConfigMongoConnectionDataElement> connDataElement = from ConfigMongoConnectionDataElement cs in connDataColl
																								 where cs.Host == Host
																								 select cs;
			if (connDataElement.Count() > 0)
			{
				return "mongodb://"
					+ (!String.IsNullOrEmpty(connDataElement.First().Username)
							&& !String.IsNullOrEmpty(connDataElement.First().Password)
						? connDataElement.First().Username + ":" + connDataElement.First().Password + "@"
						: "")
					+ connDataElement.First().DataSource
					+ ":" + connDataElement.First().Port
					+ (!String.IsNullOrEmpty(connDataElement.First().DataBase)
						? "/" + connDataElement.First().DataBase
						: "");
			}
			else if (string.IsNullOrEmpty(Host))
			{
				return "";
			}
			else
			{
				return GetConnectionString("");
			}
		}
	}
}