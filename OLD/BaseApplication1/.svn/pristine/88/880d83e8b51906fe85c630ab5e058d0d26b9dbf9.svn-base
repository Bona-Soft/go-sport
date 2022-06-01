using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration
{
	public enum HttpEnum
	{
		http, https
	}

	public class SecuritySection : ConfigurationSection
	{
		[ConfigurationProperty("application", IsRequired = true)]
		public string Application
		{
			get
			{
				return (string)this["application"];
			}
			set
			{
				this["application"] = value;
			}
		}

		[ConfigurationProperty("authEnabled", IsRequired = true)]
		public bool AuthEnabled
		{
			get
			{
				return (bool)this["authEnabled"];
			}
			set
			{
				this["authEnabled"] = value;
			}
		}

		[ConfigurationProperty("signKey")]
		public string SignKey
		{
			get
			{
				return (string)this["signKey"];
			}
			set
			{ this["signKey"] = value; }
		}

		[ConfigurationProperty("encKey")]
		public string EncKey
		{
			get
			{
				return (string)this["encKey"];
			}
			set
			{
				this["encKey"] = value;
			}
		}

		[ConfigurationProperty("cookieProtocol", DefaultValue = HttpEnum.https, IsRequired = true)]
		public HttpEnum CookieProtocol
		{
			get
			{
				return (HttpEnum)this["cookieProtocol"];
			}
			set
			{
				this["cookieProtocol"] = value;
			}
		}

		[ConfigurationProperty("cookiePath")]
		public string CookiePath
		{
			get
			{
				return (string)this["cookiePath"];
			}
			set
			{
				this["cookiePath"] = value;
			}
		}

		[ConfigurationProperty("cookieName")]
		public string CookieName
		{
			get
			{
				return (string)this["cookieName"];
			}
			set
			{
				this["cookieName"] = value;
			}
		}

		[ConfigurationProperty("sessionExp")]
		public int SessionExp
		{
			get
			{
				return (int)this["sessionExp"];
			}
			set
			{
				this["sessionExp"] = value;
			}
		}

		[ConfigurationProperty("sessionExpIWP")]
		public int SessionExpIWP
		{
			get
			{
				return (int)this["sessionExpIWP"];
			}
			set
			{
				this["sessionExpIWP"] = value;
			}
		}
	}

	public class Page : ConfigurationElement
	{
		public Page(string name)
		{
			this.Name = name;
		}

		public Page()
		{
		}

		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)this["name"];
			}
			set
			{
				this["name"] = value;
			}
		}
	}

	[ConfigurationCollection(typeof(PagesCollection), AddItemName = "page")]
	public class PagesCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new Page();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Page)element).Name;
		}

		public Page this[int index]
		{
			get
			{
				return BaseGet(index) as Page;
			}
		}

		public void Add(Page page)
		{
			BaseAdd(page);
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}
	}

	public class PagesSection : ConfigurationSection
	{
		[ConfigurationProperty("pages", IsRequired = true)]
		public PagesCollection Pages
		{
			get
			{
				return base["pages"] as PagesCollection;
			}

			set
			{
				base["pages"] = (PagesCollection)value;
			}
		}

		public PagesSection()
		{
			Page page = new Page();
			Pages.Add(page);
		}
	}
}