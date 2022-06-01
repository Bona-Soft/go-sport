using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigConnectionStringElement : ConfigurationElement
	{
		[ConfigurationProperty("connectionString", IsRequired = true, IsKey = true)]
		public string ConnectionString
		{
			get { return (string)this["connectionString"]; }
			set { this["connectionString"] = value; }
		}

		[ConfigurationProperty("providerName", DefaultValue = "SQLOLEDB")]
		public string Provider
		{
			get { return (string)this["providerName"]; }
			set { this["providerName"] = value; }
		}

		[ConfigurationProperty("host")]
		public string Host
		{
			get { return (string)this["host"]; }
			set { this["host"] = value; }
		}
	}
}