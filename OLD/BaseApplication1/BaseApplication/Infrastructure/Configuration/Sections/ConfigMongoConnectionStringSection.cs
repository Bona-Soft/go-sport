using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Commands;
using MYB.BaseApplication.Security.Configuration.Elements;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Sections
{
	public class ConfigMongoConnectionStringSection : ConfigurationSection, IConfigConnectionSection
	{
		private IBaseConfigurationBehavior _command;

		[ConfigurationProperty("", IsDefaultCollection = true)]
		private ConfigMongoConnectionStringCollection ConnectionString
		{
			get
			{
				ConfigMongoConnectionStringCollection _connectionStrings = (ConfigMongoConnectionStringCollection)base[""];
				return _connectionStrings;
			}
		}

		public IConfigConnectionCollections ConnectionStrings
		{
			get
			{
				return ConnectionString;
			}
		}

		public IBaseConfigurationBehavior Command
		{
			get
			{
				if (_command == null)
				{
					_command = new BaseConfigMongoConnectionStringsCommands();
				}
				return _command;
			}
		}
	}
}