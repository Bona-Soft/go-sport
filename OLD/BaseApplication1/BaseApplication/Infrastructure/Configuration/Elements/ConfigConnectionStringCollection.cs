using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigConnectionStringCollection : ConfigurationElementCollection, IConfigConnectionCollections
	{
		public ConfigConnectionStringCollection()
		{
			ConfigConnectionStringElement details = (ConfigConnectionStringElement)CreateNewElement();
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
			return new ConfigConnectionStringElement();
		}

		protected override Object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigConnectionStringElement)element).Host;
		}

		public ConfigConnectionStringElement this[int index]
		{
			get
			{
				return (ConfigConnectionStringElement)BaseGet(index);
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

		new public ConfigConnectionStringElement this[string name]
		{
			get
			{
				return (ConfigConnectionStringElement)BaseGet(name);
			}
		}

		public int IndexOf(ConfigConnectionStringElement details)
		{
			return BaseIndexOf(details);
		}

		public void Add(ConfigConnectionStringElement details)
		{
			BaseAdd(details);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(ConfigConnectionStringElement details)
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