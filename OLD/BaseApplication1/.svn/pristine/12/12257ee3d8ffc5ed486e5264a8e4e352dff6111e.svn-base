using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace MYB.BaseApplication.Application.CoreApplication.BaseExtensions
{
	public static class BaseEntityExt
	{
		public static T ToJson<T>(this string jsonString)
		{
			return JsonConvert.DeserializeObject<T>(jsonString);
		}

	}
}