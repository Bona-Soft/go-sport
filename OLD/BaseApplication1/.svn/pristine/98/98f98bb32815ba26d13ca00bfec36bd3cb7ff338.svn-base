using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigConnectionDataCollection : ConfigurationElementCollection, IConfigConnectionCollections
	{
		public ConfigConnectionDataCollection()
		{
			ConfigConnectionDataElement details = (ConfigConnectionDataElement)CreateNewElement();
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
			return new ConfigConnectionDataElement();
		}

		protected override Object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigConnectionDataElement)element).Host;
		}

		public ConfigConnectionDataElement this[int index]
		{
			get
			{
				return (ConfigConnectionDataElement)BaseGet(index);
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

		new public ConfigConnectionDataElement this[string name]
		{
			get
			{
				return (ConfigConnectionDataElement)BaseGet(name);
			}
		}

		public int IndexOf(ConfigConnectionDataElement details)
		{
			return BaseIndexOf(details);
		}

		public void Add(ConfigConnectionDataElement details)
		{
			BaseAdd(details);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(ConfigConnectionDataElement details)
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