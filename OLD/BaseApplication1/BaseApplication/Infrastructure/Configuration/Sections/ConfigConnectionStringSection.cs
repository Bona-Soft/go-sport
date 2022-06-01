using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Commands;
using MYB.BaseApplication.Security.Configuration.Elements;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Sections
{
	public class ConfigConnectionStringSection : ConfigurationSection, IConfigConnectionSection
	{
		private IBaseConfigurationBehavior _command;

		[ConfigurationProperty("", IsDefaultCollection = true)]
		private ConfigConnectionStringCollection ConnectionString
		{
			get
			{
				ConfigConnectionStringCollection _connectionStrings = (ConfigConnectionStringCollection)base[""];
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
					_command = new BaseConfigConnectionStringsCommands();
				}
				return _command;
			}
		}
	}
}