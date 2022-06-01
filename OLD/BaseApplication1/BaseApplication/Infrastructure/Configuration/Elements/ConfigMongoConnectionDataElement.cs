using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigMongoConnectionDataElement : ConfigurationElement
	{
		[ConfigurationProperty("host", DefaultValue = "", IsRequired = false)]
		public string Host
		{
			get { return this["host"].ToString(); }
			set { this["host"] = value; }
		}

		[ConfigurationProperty("dataSource", IsRequired = true, IsKey = true)]
		public string DataSource
		{
			get { return this["dataSource"].ToString(); }
			set { this["dataSource"] = value; }
		}

		[ConfigurationProperty("port", IsRequired = true)]
		public string Port
		{
			get { return this["port"].ToString(); }
			set { this["port"] = value; }
		}

		[ConfigurationProperty("dataBase")]
		public string DataBase
		{
			get { return this["dataBase"].ToString(); }
			set { this["dataBase"] = value; }
		}

		[ConfigurationProperty("username")]
		public string Username
		{
			get { return this["username"].ToString(); }
			set { this["username"] = value; }
		}

		[ConfigurationProperty("password")]
		public string Password
		{
			get { return this["password"].ToString(); }
			set { this["password"] = value; }
		}
	}
}