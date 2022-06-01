using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.AsyncUtil;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public abstract class BaseSSEWebServiceASHX : IHttpHandler, IReadOnlySessionState
	{
		#region " Public Properties "

		public static int SleepTimeIntervalTime = -1;
		public static int SleepTimeIntervalCount = -1;
		public static int SleepTimeIntervalRetryTime = -1;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

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

		#region " Event Handler Methods "

		public virtual void ProcessRequest(HttpContext context)
		{
			if (SleepTimeIntervalTime < 0)
			{
				SleepTimeIntervalTime = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalTime", 5000);
				SleepTimeIntervalCount = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalCount", -1);
				SleepTimeIntervalRetryTime = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalRetryTime", 10000);
			}

			if (context.Request.HttpMethod == "GET" && context.Request.AcceptTypes.Length > 0 && context.Request.AcceptTypes[0] == "text/event-stream")
			{
				ProcessSSE(context);
			}
		}

		protected virtual void AutomaticProcessSSE(HttpContext context)
		{
			context.Response.ContentType = "text/event-stream";
			context.Response.CacheControl = "no-cache";
			context.Response.Expires = -1;
			context.Response.Write("retry: " + SleepTimeIntervalRetryTime.ToString() + "\n");
			int count = 1;
			while (SleepTimeIntervalCount < 0 || count <= SleepTimeIntervalCount)
			{
				if (BaseApp.BaseUser() != null && BaseApp.BaseUser().News > 0)
				{
					int news = BaseApp.BaseUser().News;
					BaseApp.BaseUser().News = 0;

					JObject obj = new JObject();
					obj["news"] = news;
					string aux = JsonConvert.SerializeObject(obj);

					context.Response.Write("data:" + aux + "\n\n");
					context.Response.Flush();
				}

				if (context.Response.IsClientConnected == false)
				{
					break;
				}
				count++;
				System.Threading.Thread.Sleep(SleepTimeIntervalTime);
			}
		}

		protected virtual void ProcessSSE(HttpContext context)
		{
			AutomaticProcessSSE(context);
		}

		#endregion " Event Handler Methods "
	}

	public abstract class BaseSSEWebServiceASPX : System.Web.UI.Page
	{
		#region " Protected "

		protected string SubClassName
		{
			get
			{
				Type type = this.GetType().UnderlyingSystemType;
				return type.Name;
			}
		}

		private JsonSerializer jsonSerializer;
		protected JObject JResponse;
		private bool methodImplemented = false;

		protected T getRequestParam<T>(string paramName, T defaultValue)
		{
			if (Request.QueryString[paramName] != null)
			{
				return (T)Convert.ChangeType(Request.QueryString[paramName], typeof(T));
			}

			return defaultValue;
		}

		protected void sendResponse(string sContenido)
		{
			Response.Write("retry: " + SleepTimeIntervalRetryTime.ToString() + "\n");
			Response.Write("data:" + sContenido + "\n\n");
			Response.Flush();
		}

		protected void sendSerializeObject(object value)
		{
			string aux = JsonConvert.SerializeObject(value);
			Response.Write("retry: " + SleepTimeIntervalRetryTime.ToString() + "\n");
			Response.Write("data:" + aux + "\n\n");
			Response.Flush();
		}

		protected void SendSerializeMessage(string message)
		{
			JResponse = new JObject();
			JResponse["ResultCode"] = HttpContext.Current.Response.StatusCode;
			JResponse["Message"] = message;
			sendSerializeObject(JResponse);
		}

		protected void SendSerializeErrorMessage(string message)
		{
			JResponse = new JObject();
			JResponse["ResultCode"] = 520;
			JResponse["Message"] = message;
			sendSerializeObject(JResponse);
		}

		#endregion " Protected "

		#region " Public Properties "

		public static int SleepTimeIntervalTime = -1;
		public static int SleepTimeIntervalCount = -1;
		public static int SleepTimeIntervalRetryTime = -1;

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


			if (SleepTimeIntervalTime < 0)
			{
				SleepTimeIntervalTime = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalTime", 10000);
				SleepTimeIntervalCount = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalCount", 3);
				SleepTimeIntervalRetryTime = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalRetryTime", 10000);
			}

			Response.ContentType = "text/event-stream";
			Response.CacheControl = "no-cache";
			Response.Expires = -1;
			Method = Request.HttpMethod;

			if (Method == "GET" && Request.AcceptTypes.Length > 0 && Request.AcceptTypes[0] == "text/event-stream")
			{
				ProcessSSE();
			}

			AutomaticProcessSSE();

		}

		protected virtual void ProcessSSE()
		{
			AutomaticProcessSSE();
		}

		protected virtual void AutomaticProcessSSE()
		{
			int count = 0;

			while (count <= SleepTimeIntervalCount)
			{
				if (BaseUser() != null && BaseUser().News > 0)
				{
					int news = BaseUser().News;
					BaseUser().News = 0;

					JObject obj = new JObject();
					obj["news"] = news;
					sendSerializeObject(obj);
				}
				count++;
				if (Response.IsClientConnected == false)
				{
					break;
				}
				Thread.Sleep(SleepTimeIntervalTime);
			}
		}

		#endregion " Event Handlers Methods "


	}

	public abstract class BaseSSEWebServiceAsyncASPX : System.Web.UI.Page
	{
		#region " Protected "

		protected string SubClassName
		{
			get
			{
				Type type = this.GetType().UnderlyingSystemType;
				return type.Name;
			}
		}

		private JsonSerializer jsonSerializer;
		protected JObject JResponse;
		private bool methodImplemented = false;

		protected T getRequestParam<T>(string paramName, T defaultValue)
		{
			if (Request.QueryString[paramName] != null)
			{
				return (T)Convert.ChangeType(Request.QueryString[paramName], typeof(T));
			}

			return defaultValue;
		}

		protected void sendResponse(string sContenido)
		{
			Response.Write("retry: " + SleepTimeIntervalRetryTime.ToString() + "\n");
			Response.Write("data:" + sContenido + "\n\n");
			Response.Flush();
		}

		protected void sendSerializeObject(object value)
		{
			string aux = JsonConvert.SerializeObject(value);
			Response.Write("retry: " + SleepTimeIntervalRetryTime.ToString() + "\n");
			Response.Write("data:" + aux + "\n\n");
			Response.Flush();
		}

		protected void SendSerializeMessage(string message)
		{
			JResponse = new JObject();
			JResponse["ResultCode"] = HttpContext.Current.Response.StatusCode;
			JResponse["Message"] = message;
			sendSerializeObject(JResponse);
		}

		protected void SendSerializeErrorMessage(string message)
		{
			JResponse = new JObject();
			JResponse["ResultCode"] = 520;
			JResponse["Message"] = message;
			sendSerializeObject(JResponse);
		}

		#endregion " Protected "

		#region " Public Properties "

		public static int SleepTimeIntervalTime = -1;
		public static int SleepTimeIntervalCount = -1;
		public static int SleepTimeIntervalRetryTime = -1;

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

		protected async void Page_Load(object sender, EventArgs e)
		{
			string Method;


			if (SleepTimeIntervalTime < 0)
			{
				SleepTimeIntervalTime = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalTime", 10000);
				SleepTimeIntervalCount = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalCount", 3);
				SleepTimeIntervalRetryTime = BaseApp.GeneralParameters.Get("SSEWebServiceAutomaticIntervalRetryTime", 10000);
			}

			Response.ContentType = "text/event-stream";
			Response.CacheControl = "no-cache";
			Response.Expires = -1;
			Method = Request.HttpMethod;

			if (Method == "GET" && Request.AcceptTypes.Length > 0 && Request.AcceptTypes[0] == "text/event-stream")
			{
				ProcessSSE();
			}

			AutomaticProcessSSE();

		}

		protected virtual void ProcessSSE()
		{
			AutomaticProcessSSE();
		}

		protected async virtual void AutomaticProcessSSE()
		{
			int count = 0;

			while (count <= SleepTimeIntervalCount)
			{
				if (BaseUser() != null && BaseUser().News > 0)
				{
					int news = BaseUser().News;
					BaseUser().News = 0;

					JObject obj = new JObject();
					obj["news"] = news;
					sendSerializeObject(obj);
				}
				count++;
				if (Response.IsClientConnected == false)
				{
					break;
				}
				AsyncUtil.RunSync(DelayAsync);
			}
		}
		protected async virtual Task DelayAsync()
		{
			await Task.Delay(SleepTimeIntervalTime);
		}
		#endregion " Event Handlers Methods "


	}

	
}