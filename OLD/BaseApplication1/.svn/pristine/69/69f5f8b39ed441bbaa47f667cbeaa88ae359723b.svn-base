using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.Helpers.JTools
{
	public interface IJMapperObject : IDictionary<string,List<string>>
	{
		JObject ToJObject();
		bool HiddingMode { get; set; }
		void Add(string entityName, string propertyName);
	}
}
