using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.Helpers.JTools
{
	public class JMapperObject : Dictionary<string, List<string>>, IJMapperObject
	{
		public bool HiddingMode { get; set; }
		public JMapperObject(bool hiddingMode)
		{
			HiddingMode = hiddingMode;
		}
		public JMapperObject()
		{
			HiddingMode = true;
		}

		public JObject ToJObject()
		{
			JObject obj = new JObject();
			foreach (var item in this)
			{
				obj[item.Key] = new JObject();
				foreach (string propertyName in item.Value)
				{
					obj[item.Key][propertyName] = null;
				}
			}
			return obj;
		}

		public void Add(string entityName, string propertyName)
		{
			if (base.ContainsKey(entityName))
			{
				if (base[entityName] == null)
				{
					base[entityName] = new List<string>();
				}
				base[entityName].Add(propertyName);
			}
			else
			{
				base.Add(entityName, new List<string>() { propertyName });
			}
		}
	}
}
