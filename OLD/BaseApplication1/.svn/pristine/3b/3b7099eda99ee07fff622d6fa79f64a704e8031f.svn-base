using System;
using System.Configuration;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigResolveHostListCollection : ConfigurationElementCollection
	{
		public ConfigResolveHostListCollection()
		{
			ConfigResolveHostListElement details = (ConfigResolveHostListElement)CreateNewElement();
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
			return new ConfigResolveHostListElement();
		}

		protected override Object GetElementKey(ConfigurationElement element)
		{
			return ((ConfigResolveHostListElement)element).Host;
		}

		public ConfigResolveHostListElement this[int index]
		{
			get
			{
				return (ConfigResolveHostListElement)BaseGet(index);
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

		new public ConfigResolveHostListElement this[string name]
		{
			get
			{
				return (ConfigResolveHostListElement)BaseGet(name);
			}
		}

		public int IndexOf(ConfigResolveHostListElement details)
		{
			return BaseIndexOf(details);
		}

		public void Add(ConfigResolveHostListElement details)
		{
			BaseAdd(details);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(ConfigResolveHostListElement details)
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
			get { return "domain"; }
		}
	}
}