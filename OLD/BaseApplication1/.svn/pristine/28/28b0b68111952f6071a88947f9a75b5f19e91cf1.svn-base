using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Elements;
using System.Collections.Generic;
using System.Linq;

namespace MYB.BaseApplication.Security.Configuration.Commands
{
	public class BaseConfigConnectionStringsCommands : IBaseConfigurationBehavior
	{
		public string GetConnectionString()
		{
			return GetConnectionString("");
		}

		public string GetConnectionString(string Host)
		{
			IConfigConnectionCollections connStringColl = BaseConfigurationManager.ConnectionSection.ConnectionStrings;
			IEnumerable<ConfigConnectionStringElement> connStringElement = from ConfigConnectionStringElement cs in connStringColl
																								where cs.Host == Host
																								select cs;
			if (connStringElement.Count<ConfigConnectionStringElement>() > 0)
			{
				return connStringElement.First<ConfigConnectionStringElement>().ConnectionString;
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