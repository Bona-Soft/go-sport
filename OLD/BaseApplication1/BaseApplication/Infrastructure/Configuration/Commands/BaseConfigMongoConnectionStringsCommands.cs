using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Elements;
using System.Collections.Generic;
using System.Linq;

namespace MYB.BaseApplication.Security.Configuration.Commands
{
	public class BaseConfigMongoConnectionStringsCommands : IBaseConfigurationBehavior
	{
		public string GetConnectionString()
		{
			return GetConnectionString("");
		}

		public string GetConnectionString(string Host)
		{
			IConfigConnectionCollections connStringColl = BaseConfigurationManager.MongoConnectionSection.ConnectionStrings;
			IEnumerable<ConfigMongoConnectionStringElement> connStringElement = from ConfigMongoConnectionStringElement cs in connStringColl
																									  where cs.Host == Host
																									  select cs;
			if (connStringElement.Count() > 0)
			{
				return connStringElement.First().ConnectionString;
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