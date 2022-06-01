using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Web;

namespace MYB.BaseApplication.Domain.ExceptionManagaer
{
	public class ErrorManager : IErrorManager
	{
		private JsonSerializer jsonSerializer;

		public JArray jsonError { get; private set; }

		protected JsonSerializer Serializer
		{
			get
			{
				if (jsonSerializer == null)
				{
					JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
					jsonSerializer = JsonSerializer.Create(serializerSettings);
				}
				return jsonSerializer;
			}
		}

		public ErrorManager()
		{
			jsonError = new JArray();
		}

		public bool IsNotLoggedError(string errorMessage)
		{
			if (!BaseApp.Session().IsLogged)
			{
				BaseApp.ErrorManager.AddError(errorMessage);
			}
			return !BaseApp.Session().IsLogged;
		}

		public bool EmailError(string email, string errorMessage)
		{
			bool isEmail = Validator.IsMail(email);
			if (!isEmail)
			{
				AddError(errorMessage);
			}
			return !isEmail;
		}
		
		public bool MandatoryEmailError<T>(JObject data, string propertyName, string errorMandatoryMessage, string errorFormatMessage)
		{
			T propertyObject;

			try
			{
				if (data[propertyName] == null || String.IsNullOrEmpty(data[propertyName].ToString()))
				{
					AddError(errorMandatoryMessage);
					return true;
				}

				propertyObject = (T)Convert.ChangeType(data[propertyName], typeof(T));
			}
			catch
			{
				AddError(errorMandatoryMessage);
				return true;
			}
			if (!MandatoryError<T>(propertyObject, errorMandatoryMessage))
			{
				return EmailError(data[propertyName].ToString(), errorFormatMessage);
			}
			return true;
		}

		public bool MandatoryEmailError<T>(T field, string errorMandatoryMessage, string errorFormatMessage)
		{
			bool result = MandatoryError(field);
			if (result)
			{
				AddError(errorMandatoryMessage);
			}
			else
			{
				try
				{
					result = EmailError(field.ToString(), errorFormatMessage);
				}
				catch
				{
					result = true;
					AddError(errorFormatMessage);
				}
				
			}
			return result;
		}

		public bool MinCountError(JArray data, int minCount) 
		{
			return data == null || data.Equals(default(JArray)) || data.Count < minCount;  
		}
		public bool MinCountError(JArray data, int minCount, string errorMessage)
		{
			bool result = MinCountError(data, minCount);
			if (result)
			{
				AddError(errorMessage);
			}
			return result;
		}

		public bool MandatoryError<T>(Filter filter, string keyName, string errorMessage)
		{
			T propertyObject;

			try
			{
				if (filter == null || !filter.ContainsKey(keyName) || String.IsNullOrEmpty(filter[keyName].ToString()) || filter[keyName].ToDefType<T>().Equals(default(T)))
				{
					AddError(errorMessage);
					return true;
				}

				propertyObject = (T)Convert.ChangeType(filter[keyName], typeof(T));
			}
			catch
			{
				AddError(errorMessage);
				return true;
			}
			return MandatoryError(propertyObject, errorMessage);
		}

		public bool MandatoryError<T>(T field)
		{
			if (typeof(T) == typeof(string))
				return string.IsNullOrEmpty(field as string);
			return field == null || field.Equals(default(T));
		}

		
		public bool MandatoryError<T,TID>(JObject data, string objectName, string propertyName,  string errorMessage)
		{
			T propertyObject;

			try
			{
				if (data[objectName] == null || String.IsNullOrEmpty(data[objectName].ToString()) || data[objectName][propertyName].ToDefType<TID>().Equals(default(TID)))
				{
					AddError(errorMessage);
					return true;
				}

				propertyObject = (T)Convert.ChangeType(data[objectName], typeof(T));
			}
			catch
			{
				AddError(errorMessage);
				return true;
			}
			return MandatoryError(propertyObject, errorMessage);
		}

		public bool MandatoryError<T>(JObject data,string propertyName, string errorMessage)
		{
			T propertyObject;

			try
			{
				if (data[propertyName] == null || String.IsNullOrEmpty(data[propertyName].ToString()))
				{
					AddError(errorMessage);
					return true;
				}

				propertyObject = (T)Convert.ChangeType(data[propertyName], typeof(T));
			}
			catch
			{
				AddError(errorMessage);
				return true;
			}
			return MandatoryError(propertyObject, errorMessage);
		}

		public bool MandatoryError<T>(T field, string errorMessage)
		{
			bool result = MandatoryError(field);
			if (result)
			{
				AddError(errorMessage);
			}
			return result;
		}

		public void AddError(string errorMesssage) => AddError(errorMesssage, 1);

		public void AddError(string errorMesssage, int errorCode)
		{
			JObject jo = new JObject();
			jo["ErrorCode"] = errorCode;
			jo["Message"] = errorMesssage;
			jsonError.Add(jo);
		}

		public void Verify()
		{
			if (jsonError.Count > 0 && HttpContext.Current != null)
			{
				HttpContext.Current.Response.StatusCode = 520;
            Serializer.Serialize(HttpContext.Current.Response.Output, jsonError);
				HttpContext.Current.Response.End();
			}
		}
	}
}