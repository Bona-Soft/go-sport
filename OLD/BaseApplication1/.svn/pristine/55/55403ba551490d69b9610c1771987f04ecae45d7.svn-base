using MYB.BaseApplication.Application.CoreApplication.BaseExtensions;
using MYB.BaseApplication.Application.CoreInterfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseEntity : IBaseEntity
	{
		public void FillFrom<T>( T target, T source) where T: IBaseEntity
		{
			Type t = typeof(T);
			
			var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

			foreach (var prop in properties)
			{
				var value = prop.GetValue(source, null);
				if (value != null)
					prop.SetValue(target, value, null);
			}
		}

		public void FillFrom<T>(T source)
		{
			Type t = typeof(T);

			var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

			foreach (var prop in properties)
			{
				var value = prop.GetValue(source, null);
				if (value != null)
					prop.SetValue(this, value, null);
			}
		}

		public BaseEntity(object ID)
		{

		}

		public BaseEntity()
		{

		}
	}
}