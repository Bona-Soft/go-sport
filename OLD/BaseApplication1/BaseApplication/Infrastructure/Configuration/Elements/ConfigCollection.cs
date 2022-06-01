using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Configuration;
using System.Reflection;

namespace MYB.BaseApplication.Security.Configuration.Elements
{
	public class ConfigCollection<TConfigElement> : ConfigurationElementCollection, IConfigConnectionCollections where TConfigElement : ConfigurationElement
	{
		public ConfigCollection()
		{
			TConfigElement details = (TConfigElement)CreateNewElement();
			if (details != null)
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
			ConstructorInfo constructor = typeof(TConfigElement).GetConstructor(new Type[] { });
			TConfigElement ConfigElement = constructor.Invoke(new object[] { }) as TConfigElement;
			//return (TConfigElement)Activator.CreateInstance(typeof(TConfigElement), new object[] { });
			return ConfigElement;
		}

		protected override Object GetElementKey(ConfigurationElement element)
		{
			return ((TConfigElement)element);
		}

		public TConfigElement this[int index]
		{
			get
			{
				return (TConfigElement)BaseGet(index);
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

		new public TConfigElement this[string name]
		{
			get
			{
				return (TConfigElement)BaseGet(name);
			}
		}

		public int IndexOf(TConfigElement details)
		{
			return BaseIndexOf(details);
		}

		public void Add(TConfigElement details)
		{
			BaseAdd(details);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(TConfigElement details)
		{
			if (BaseIndexOf(details) >= 0)
				BaseRemove(details);
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
			get
			{
				return BaseConfigurationManager<TConfigElement>._elementName;
			}
		}
	}
}