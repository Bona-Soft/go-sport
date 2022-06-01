using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Security.Configuration.Commands;

namespace MYB.BaseApplication.Security.Configuration.Sections
{
	public class ConfigMainConnectionStringSection : IConfigConnectionSection
	{
		private IBaseConfigurationBehavior _command;

		public IConfigConnectionCollections ConnectionStrings
		{
			get
			{
				return null;
			}
		}

		public IBaseConfigurationBehavior Command
		{
			get
			{
				if (_command == null)
				{
					_command = new BaseConfigMainConnectionStringCommands();
				}
				return _command;
			}
		}
	}
}