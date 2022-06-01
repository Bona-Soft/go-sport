using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration
{
	public class ConnectionData : ConfigurationSection
	{
		[ConfigurationProperty("host", DefaultValue = "", IsRequired = false)]
		public string Host
		{
			get { return this["host"].ToString(); }
			set { this["host"] = value; }
		}

		[ConfigurationProperty("provider", DefaultValue = "SQLOLEDB")]
		public string Provider
		{
			get { return this["provider"].ToString(); }
			set { this["provider"] = value; }
		}

		[ConfigurationProperty("dataSource")]
		public string DataSource
		{
			get { return this["dataSource"].ToString(); }
			set { this["dataSource"] = value; }
		}

		[ConfigurationProperty("initialCatalog")]
		public string InitialCatalog
		{
			get { return this["initialCatalog"].ToString(); }
			set { this["initialCatalog"] = value; }
		}

		[ConfigurationProperty("userID")]
		public string UserID
		{
			get { return this["userID"].ToString(); }
			set { this["userID"] = value; }
		}

		[ConfigurationProperty("password")]
		public string Password
		{
			get { return this["password"].ToString(); }
			set { this["password"] = value; }
		}
	}
}