using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public abstract class BaseWebService : System.Web.UI.Page
	{
		private JsonSerializer jsonSerializer;
		protected JObject JResponse;
		private bool methodImplemented = false;

		#region " Protected Properties "

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

		private JObject _Data;

		protected JObject Data
		{
			get
			{
				if (_Data == null)
					_Data = getRequestJObject();
				return _Data;
			}
		}

		#endregion " Protected Properties "

		#region " Protected Methods "

		protected JObject FromObj(object source)
		{
			return FromObj(source, new JObject());
		}

		protected JObject FromObj(object source, JObject destinationView)
		{
			string name;
			JToken value;
			object propertyValue;

			if (source == null)
			{
				return destinationView;
			}
			if (destinationView.Count == 0)
			{
				foreach (var item in source.GetType().GetProperties())
				{
					destinationView[item.Name] = JValue.FromObject(item.GetValue(source, null));
				}
			}
			else
			{
				foreach (var item in destinationView)
				{
					name = item.Key;
					value = item.Value;

					if (value.Type == JTokenType.Array)
					{
						//destinationView[name] = new JArray();
						//JArray ja = new JArray();
						//foreach()
						//JObject jobj = FromObj(source, item)
						//ja.Add()

						continue;
					}
					else if (value.Type == JTokenType.Object)
					{
						JObject jobj = FromObj(source.GetType().GetProperty(name).GetValue(source, null), (JObject)item.Value);
						destinationView[name] = jobj;
					}
					else
					{
						PropertyInfo Property = source.GetType().GetProperty(name);
						if (Property != null)
						{
							propertyValue = Property.GetValue(source, null);
							if (propertyValue != null)
							{
								destinationView[name] = JValue.FromObject(propertyValue);
							}
							else
							{
								destinationView[name] = null;
							}
						}
					}
				}
			}

			return destinationView;
		}

		protected T getRequestParam<T>(string paramName, T defaultValue)
		{
			if (Request.QueryString[paramName] != null)
			{
				return (T)Convert.ChangeType(Request.QueryString[paramName], typeof(T));
			}

			return defaultValue;
		}

      protected Filter getRequestFilter()
      {
         Filter filter = new Filter();
         foreach(string key in Request.QueryString)
         {
				if(key != null)
				{
					filter[key] = Request.QueryString[key];
				}
            
         }
         return filter;
      } 
		protected string getRequestData()
		{
			var jsonString = String.Empty;

			Request.InputStream.Position = 0;
			using (var inputStream = new StreamReader(Request.InputStream))
			{
				jsonString = inputStream.ReadToEnd();
			}

			
			return jsonString;
		}

		protected JObject getRequestJObject()
		{
			JObject data = new JObject();
			string rData = getRequestData();
			if (rData != String.Empty)
         {
            try
            {
               data = JObject.Parse(rData);
            }
            catch
            {
               
            }
         }
				
			return data;
		}

		protected JObject getRequestJObject(string objectName)
		{
			JObject Data = new JObject();
			if (Request[objectName] != null)
			{
            try
            {
               Data = JObject.Parse(Request[objectName]);
            }
            catch { }
			}
			return Data;
		}

		protected JArray getRequestJArray()
		{
			JArray Data = new JArray();

			string rData = getRequestData();
			if (rData != String.Empty)
         {
            try
            {
               Data = JArray.Parse(rData);
            }
            catch { }
         }
				
			return Data;
		}

		protected void sendJResponse(string sContenido)
		{
			JResponse = JObject.Parse(sContenido);
			Serializer.Serialize(Response.Output, JResponse);
			Response.Flush();
		}

		protected void sendSerializeObject(object value)
		{
			Serializer.Serialize(Response.Output, value);
			Response.Flush();
		}

		protected void SendSerializeMessage(string message)
		{
			JResponse = new JObject();
			JResponse["ResultCode"] = HttpContext.Current.Response.StatusCode;
			JResponse["Message"] = message;
			Serializer.Serialize(Response.Output, message);
			Response.Flush();
		}

		protected void SendSerializeErrorMessage(string message)
		{
			JResponse = new JObject();
			JResponse["ResultCode"] = 520;
			JResponse["Message"] = message;
			Serializer.Serialize(Response.Output, message);
			Response.Flush();
		}
		
		#endregion " Protected Methods "

		#region " Public Properties "

		public static bool IsLogged
		{
			get
			{
				return AppSession.IsLogged;
			}
		}

		public static int CurrentImplementation
		{
			get
			{
				return BaseApp.CurrentImplementation;
			}
		}

		public static IContainerManager ContainerManager
		{
			get
			{
				return BaseApp.ContainerManager;
			}
		}

		public static IAppSession AppSession
		{
			get
			{
				return BaseApp.Session();
			}
		}

		public static IDataBase DB
		{
			get
			{
				return BaseApp.DB;
			}
		}

		public static IWSP WSP
		{
			get
			{
				return BaseApp.WSP;
			}
		}

		public static IVirtualPagesManager VirtualPageManager
		{
			get
			{
				return BaseApp.VirtualPageManager;
			}
		}

		public static IBaseUserManager BaseUserManager
		{
			get
			{
				return BaseApp.BaseUserManager;
			}
		}

		public static IBaseConfigurationManager ConfigManager
		{
			get
			{
				return BaseApp.BaseConfigManager();
			}
		}

		public static IErrorManager ErrorManager
		{
			get
			{
				return BaseApp.ErrorManager;
			}
		}

		public static ITranslationManager TranslationManager
		{
			get
			{
				return BaseApp.TranslationManager;
			}
		}

		#endregion " Public Properties "

		#region " Public Methods "

		public static IBaseUser BaseUser(long userID, int implementationID)
		{
			return BaseApp.BaseUser(userID, implementationID);
		}

		public static IBaseUser BaseUser(long userID)
		{
			return BaseApp.BaseUser(userID);
		}

		public static IBaseUser BaseUser()
		{
			return AppSession.User;
		}

		public static Interface Resolve<Interface>()
		{
			return BaseApp.Resolve<Interface>();
		}

		public static T UI<T>() where T : IBaseUIService
		{
			return BaseApp.UI<T>();
		}

		#endregion " Public Methods "

		#region " Event Handlers Methods "

		protected void Page_Load(object sender, EventArgs e)
		{
			string Method;

			Response.ContentType = "application/json";
			Method = Request.HttpMethod;

			if (Method == "GET")
			{
				ProcessGET();
			}
			if (Method == "POST")
			{
				ProcessPOST();
			}
			if (Method == "PUT")
			{
				ProcessPUT();
			}
			if (Method == "DELETE")
			{
				ProcessDELETE();
			}

			Response.End();
		}

      #endregion " Event Handlers Methods "

      #region " Process Methods "
      protected virtual void ProcessGET(Filter filter)
      {
         sendJResponse("{\"Error\":\"" + TranslationManager.Get("MethodNotImplemented", "Method not implemented") + "\"}");
      }

      protected virtual void ProcessGET()
		{
         ProcessGET(getRequestFilter());
      }

      protected virtual void ProcessPOST(JObject data)
      {
			ProcessPOST(getRequestJArray());
		}

		protected virtual void ProcessPOST(JArray data)
		{
			sendJResponse("{\"Error\":\"" + TranslationManager.Get("MethodNotImplemented", "Method not implemented") + "\"}");
		}

		protected virtual void ProcessPOST()
		{
         ProcessPOST(getRequestJObject());
		}

		protected virtual void ProcessPUT()
		{
			sendJResponse("{\"Error\":\"" + TranslationManager.Get("MethodNotImplemented", "Method not implemented") + "\"}");
		}

		protected virtual void ProcessDELETE()
		{
			sendJResponse("{\"Error\":\"" + TranslationManager.Get("MethodNotImplemented", "Method not implemented") + "\"}");
		}

		#endregion " Process Methods "
	}

}