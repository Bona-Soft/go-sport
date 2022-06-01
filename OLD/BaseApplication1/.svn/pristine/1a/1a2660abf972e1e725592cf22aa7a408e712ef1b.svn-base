using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Elements;
using System.Collections.Generic;
using System.Linq;

namespace MYB.BaseApplication.Security.Configuration.Commands
{
	public class BaseConfigConnectionDataCommands : IBaseConfigurationBehavior
	{
		public string GetConnectionString()
		{
			return GetConnectionString("");
		}

		public string GetConnectionString(string Host)
		{
			IConfigConnectionCollections connDataColl = BaseConfigurationManager.ConnectionSection.ConnectionStrings;
			IEnumerable<ConfigConnectionDataElement> connDataElement = from ConfigConnectionDataElement cs in connDataColl
																						  where cs.Host == Host
																						  select cs;
			if (connDataElement.Count<ConfigConnectionDataElement>() > 0)
			{
				return "Provider=" + connDataElement.First<ConfigConnectionDataElement>().Provider + ";"
							+ "Data Source=" + connDataElement.First<ConfigConnectionDataElement>().DataSource + ";"
							+ "Initial Catalog=" + connDataElement.First<ConfigConnectionDataElement>().InitialCatalog + ";"
							+ "User Id=" + connDataElement.First<ConfigConnectionDataElement>().UserID + ";"
							+ "Password=" + connDataElement.First<ConfigConnectionDataElement>().Password;
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