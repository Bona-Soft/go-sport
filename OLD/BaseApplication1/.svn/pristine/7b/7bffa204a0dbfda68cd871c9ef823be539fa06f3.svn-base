using System.Configuration;

namespace MYB.BaseApplication.Infrastructure.TestApp
{
	public class TestConfigElement : ConfigurationElement
	{
		[ConfigurationProperty("ID", IsRequired = true)]
		public string ID
		{
			get { return (string)this["ID"]; }
			set { this["ID"] = value; }
		}

		[ConfigurationProperty("Name")]
		public string Name
		{
			get { return (string)this["Name"]; }
			set { this["Name"] = value; }
		}
	}
}