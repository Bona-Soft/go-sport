using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigMongoConnectionStringCollection : ConfigurationElementCollection, IConfigConnectionCollections
	{
		public ConfigMongoConnectionStringCollection()
		{
			ConfigMongoConnectionStringElement details = (ConfigMongoConnectionStringElement)CreateNewElement();
			if (details.Host != "")
			{
				Add(details);
			}
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ConfigMongoConnectionStringElement();
		}

		protected override Object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigMongoConnectionStringElement)element).Host;
		}

		public ConfigMongoConnectionStringElement this[int index]
		{
			get
			{
				return (ConfigMongoConnectionStringElement)BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		new public ConfigMongoConnectionStringElement this[string name]
		{
			get
			{
				return (ConfigMongoConnectionStringElement)BaseGet(name);
			}
		}

		public int IndexOf(ConfigMongoConnectionStringElement details)
		{
			return BaseIndexOf(details);
		}

		public void Add(ConfigMongoConnectionStringElement details)
		{
			BaseAdd(details);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(ConfigMongoConnectionStringElement details)
		{
			if (BaseIndexOf(details) >= 0)
				BaseRemove(details.Host);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override string ElementName
		{
			get { return "connectionString"; }
		}
	}
}