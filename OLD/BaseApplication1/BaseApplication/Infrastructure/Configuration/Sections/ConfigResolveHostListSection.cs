using MYB.BaseApplication.Security.Configuration.Elements;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Sections
{
	public class ConfigResolveHostListSection : ConfigurationSection
	{
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ConfigResolveHostListCollection ResolveHostList
		{
			get
			{
				ConfigResolveHostListCollection _domain = (ConfigResolveHostListCollection)base[""];
				return _domain;
			}
		}
	}
}