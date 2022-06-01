using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigMongoConnectionDataCollection : ConfigurationElementCollection, IConfigConnectionCollections
	{
		public ConfigMongoConnectionDataCollection()
		{
			ConfigMongoConnectionDataElement details = (ConfigMongoConnectionDataElement)CreateNewElement();
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
			return new ConfigMongoConnectionDataElement();
		}

		protected override Object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigMongoConnectionDataElement)element).Host;
		}

		public ConfigMongoConnectionDataElement this[int index]
		{
			get
			{
				return (ConfigMongoConnectionDataElement)BaseGet(index);
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

		new public ConfigMongoConnectionDataElement this[string name]
		{
			get
			{
				return (ConfigMongoConnectionDataElement)BaseGet(name);
			}
		}

		public int IndexOf(ConfigMongoConnectionDataElement details)
		{
			return BaseIndexOf(details);
		}

		public void Add(ConfigMongoConnectionDataElement details)
		{
			BaseAdd(details);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(ConfigMongoConnectionDataElement details)
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
			get { return "connectionData"; }
		}
	}
}