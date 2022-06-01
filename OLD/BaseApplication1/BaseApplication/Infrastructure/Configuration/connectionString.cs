using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration
{
	public class connectionString : ConfigurationSection
	{
		[ConfigurationProperty("string")]
		public string String
		{
			get { return this["string"].ToString(); }
			set { this["string"] = value; }
		}

		[ConfigurationProperty("host", DefaultValue = "", IsRequired = false)]
		public string Host
		{
			get { return this["host"].ToString(); }
			set { this["host"] = value; }
		}

		[ConfigurationProperty("provider", DefaultValue = "SQLOLEDB", IsRequired = false)]
		public string Provider
		{
			get { return this["provider"].ToString(); }
			set { this["provider"] = value; }
		}
	}
}