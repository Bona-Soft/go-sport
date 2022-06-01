using MYB.BaseApplication.Application.CoreInterfaces;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Commands
{
	public class BaseConfigMongoMainConnectionStringCommands : IBaseConfigurationBehavior
	{
		public string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["MongoConnectionString"].ConnectionString;
		}

		public string GetConnectionString(string Host)
		{
			return GetConnectionString();
		}
	}
}