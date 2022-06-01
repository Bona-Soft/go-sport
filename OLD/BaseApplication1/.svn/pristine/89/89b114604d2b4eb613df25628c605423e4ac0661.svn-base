using MYB.BaseApplication.Application.CoreInterfaces;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Commands
{
	public class BaseConfigMainConnectionStringCommands : IBaseConfigurationBehavior
	{
		public string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
		}

		public string GetConnectionString(string Host)
		{
			return GetConnectionString();
		}
	}
}