using MYB.BaseApplication.Security.Configuration.Elements;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Sections
{
	public class ConfigSection<TConfigElement> : ConfigurationSection where TConfigElement : ConfigurationElement
	{
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ConfigCollection<TConfigElement> ConfigColl
		{
			get
			{
				ConfigCollection<TConfigElement> _configColl = (ConfigCollection<TConfigElement>)base[""];
				return _configColl;
			}
		}
	}
}