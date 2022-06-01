using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigResolveHostListElement : ConfigurationElement
	{
		[ConfigurationProperty("ID", IsRequired = true)]
		public string ID
		{
			get { return (string)this["ID"]; }
			set { this["ID"] = value; }
		}

		[ConfigurationProperty("host", IsRequired = true)]
		public string Host
		{
			get { return (string)this["host"]; }
			set { this["host"] = value; }
		}
	}
}