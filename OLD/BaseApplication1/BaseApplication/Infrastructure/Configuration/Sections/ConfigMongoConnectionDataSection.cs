using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Commands;
using MYB.BaseApplication.Security.Configuration.Elements;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Sections
{
	public class ConfigMongoConnectionDataSection : ConfigurationSection, IConfigConnectionSection
	{
		private IBaseConfigurationBehavior _command;

		[ConfigurationProperty("", IsDefaultCollection = true)]
		private ConfigConnectionDataCollection ConnectionData
		{
			get
			{
				ConfigConnectionDataCollection _connectionStrings = (ConfigConnectionDataCollection)base[""];
				return _connectionStrings;
			}
		}

		public IConfigConnectionCollections ConnectionStrings
		{
			get
			{
				return ConnectionData;
			}
		}

		public IBaseConfigurationBehavior Command
		{
			get
			{
				if (_command == null)
				{
					_command = new BaseConfigConnectionDataCommands();
				}
				return _command;
			}
		}
	}
}