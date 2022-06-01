using MongoDB.Bson;
using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.TestApp
{
	public class TestPage : IVirtualPage
	{
		public void Register()
		{
			BaseApp.VirtualPageManager.RegisterPage("CheckWindsor.aspx", HttpMethod.Post, CheckWidnsor);
			BaseApp.VirtualPageManager.RegisterPage("CheckConfig.aspx", HttpMethod.Post, CheckConfig);
			BaseApp.VirtualPageManager.RegisterPage("CheckSP.aspx", HttpMethod.Post, CheckSP);
			BaseApp.VirtualPageManager.RegisterPage("CheckConstructor.aspx", HttpMethod.Post, CheckConstructor);
			BaseApp.VirtualPageManager.RegisterPage("InterceptorTest.aspx", HttpMethod.Post, InterceptorTest);
			BaseApp.VirtualPageManager.RegisterPage("CheckGP.aspx", HttpMethod.Post, CheckGP);
			BaseApp.VirtualPageManager.RegisterPage("MongoDB.aspx", HttpMethod.Post, MongoDB);
		}

		public static bool MongoDB(HttpRequest Request, HttpResponse Response)
		{
			BsonDocument bd = BaseApp.Session().ToBsonDocument();
			BaseApp.MongoDB().Collection<BsonDocument>("TablaOColleccion").InsertOne(bd);
			return true;
		}

		public static bool CheckGP(HttpRequest Request, HttpResponse Response)
		{
			BaseApp.WSP.SessionGroup.Logout(1, "a").Execute();
			BaseApp.BaseConfigManager().GeneralParameter().Set("parametroDePrueba", 120);
			BaseApp.BaseConfigManager().GeneralParameter().Set("parametroDePrueba2", "http://bonait.com.ar");
			int gp = BaseApp.BaseConfigManager().GeneralParameter().Get<int>("parametroDePrueba");
			string gpStr = BaseApp.BaseConfigManager().GeneralParameter().Get<string>("parametroDePruebaQueNoExiste");
			gpStr = BaseApp.BaseConfigManager().GeneralParameter().Get<string>("parametroDePrueba2");
			object gp2 = BaseApp.BaseConfigManager().GeneralParameter().Get("parametroDePrueba");
			return true;
		}

		public static bool InterceptorTest(HttpRequest Request, HttpResponse Response)
		{
			BaseApp.UI<TestUIService>().Algo();

			return true;
		}

		public static bool CheckConstructor(HttpRequest Request, HttpResponse Response)
		{
			return true;
		}

		public static bool CheckWidnsor(HttpRequest Request, HttpResponse Response)
		{
			Response.Write("<html><body>");

			return true;
		}

		public static bool CheckConfig(HttpRequest Request, HttpResponse Response)
		{
			IEnumerable<TestConfigElement> elements = BaseApp.BaseConfigManager<TestConfigElement>().GetSectionElementList("CustomConfigSection", "testConfig");

			Response.Write(elements.Last().ID.ToString());
			return true;
		}

		public static bool CheckSP(HttpRequest Request, HttpResponse Response)
		{
			string session1 = Guid.NewGuid().ToString();
			string session2 = Guid.NewGuid().ToString();
			string session3 = Guid.NewGuid().ToString();
			string result = "<html>";

			try
			{
				/* result += "<strong>AddUser:</strong> " + BaseApp.WSP.UserGroup
					 .AddUser("AutoTestUser", "I3DVYBBIQeTquK9oVHRzIuLWrTe0CVJxyFR6IHqAVyptzzCb3LhHFdAWzae46aMcBwAAAA==")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>AddUser:</strong> " +
					 BaseApp.WSP.UserGroup
					 .AddUser(1, "AutoTestUser", "I3DVYBBIQeTquK9oVHRzIuLWrTe0CVJxyFR6IHqAVyptzzCb3LhHFdAWzae46aMcBwAAAA==")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>AddUser:</strong> " +
					 BaseApp.WSP.UserGroup
					 .AddUser(1, "AutoTestUserImp1", "I3DVYBBIQeTquK9oVHRzIuLWrTe0CVJxyFR6IHqAVyptzzCb3LhHFdAWzae46aMcBwAAAA==")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>AddUser:</strong> " +
					 BaseApp.WSP.UserGroup
					 .AddUser(2, "AutoTestUserImp2", "I3DVYBBIQeTquK9oVHRzIuLWrTe0CVJxyFR6IHqAVyptzzCb3LhHFdAWzae46aMcBwAAAA==")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>ExistsUsername:</strong> " +
					 BaseApp.WSP.UserGroup
					 .ExistsUsername("AutoTestUserImp2")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>ExistsUsername:</strong> " +
					 BaseApp.WSP.UserGroup
					 .ExistsUsername("AutoTestUser")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>AddUserLogin:</strong> " +
					 BaseApp.WSP.UserGroup
					 .AddUserLogin(1, "AutoTestAddUserLogin", "I3DVYBBIQeTquK9oVHRzIuLWrTe0CVJxyFR6IHqAVyptzzCb3LhHFdAWzae46aMcBwAAAA==")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>AddUserLogin:</strong> " +
					 BaseApp.WSP.UserGroup
					 .AddUserLogin(10, "AutoTestAddUserLogin", "I3DVYBBIQeTquK9oVHRzIuLWrTe0CVJxyFR6IHqAVyptzzCb3LhHFdAWzae46aMcBwAAAA==")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>DeleteLogin:</strong> " +
					 BaseApp.WSP.UserGroup
					 .DeleteLogin(1, "AutoTestAddUserLogin")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>DeleteLogin:</strong> " +
					 BaseApp.WSP.UserGroup
					 .DeleteLogin("AutoTestAddUserLogin")
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 /*result += "<strong>SetSession:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .SetSession(1, session1, Request.UserHostAddress, false)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>SetSession:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .SetSession(10, session2, Request.UserHostAddress, false)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>SetSession:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .SetSession(9, session3, Request.UserHostAddress, false)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";
					 *
				 result += "<strong>AuthenticateSession:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .AuthenticateSession(session1, Request.UserHostAddress)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>AuthenticateSession:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .AuthenticateSession(session2, Request.UserHostAddress)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>GetLoginPasswords:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .GetLoginPasswords("AutoTestUser", Request.UserHostAddress)
					 .Execute(BaseApp.DB)
					 .Rows.Count
					 .ToString() + "<br>";

				 result += "<strong>GetLoginPasswords:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .GetLoginPasswords(1, "AutoTestUser", Request.UserHostAddress)
					 .Execute(BaseApp.DB)
					 .Rows.Count
					 .ToString() + "<br>";

				 result += "<strong>GetLoginPasswords:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .GetLoginPasswords(1, "AutoTest", Request.UserHostAddress)
					 .Execute(BaseApp.DB)
					 .Rows.Count
					 .ToString() + "<br>";

				 result += "<strong>Logout:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .Logout(1, session1, false)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>Logout:</strong> " +
					 BaseApp.WSP.SessionGroup
					 .Logout(session2, false)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>EnumUserLogins:</strong> " +
					 BaseApp.WSP.UserGroup
					 .EnumUserLogins(1)
					 .Execute(BaseApp.DB)
					 .Tables[0].Rows.Count
					 .ToString() + "<br>";

				 result += "<strong>EnumUserLogins:</strong> " +
					 BaseApp.WSP.UserGroup
					 .EnumUserLogins(10)
					 .Execute(BaseApp.DB)
					 .Tables[0].Rows.Count
					 .ToString() + "<br>";

				 result += "<strong>EnumUserSessions:</strong> " +
					 BaseApp.WSP.UserGroup
					 .EnumUserSessions(1)
					 .Execute(BaseApp.DB)
					 .Tables[0].Rows.Count
					 .ToString() + "<br>";

				 result += "<strong>RemoveSession:</strong> " +
					 BaseApp.WSP.UserGroup
					 .RemoveSession(session1)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>RemoveSession:</strong> " +
					 BaseApp.WSP.UserGroup
					 .RemoveSession(session2)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>RemoveAllUserSession:</strong> " +
					 BaseApp.WSP.UserGroup
					 .RemoveAllUserSession(2)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>RemoveSession:</strong> " +
					 BaseApp.WSP.UserGroup
					 .RemoveSession(session3)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>RemoveAllUserSession:</strong> " +
					 BaseApp.WSP.UserGroup
					 .RemoveAllUserSession(10)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>DisconnectUser:</strong> " +
					 BaseApp.WSP.UserGroup
					.DisconnectUser(1, false)
					.Execute(BaseApp.DB)
					.ToString() + "<br>";

				 result += "<strong>DeleteUser:</strong> " +
					 BaseApp.WSP.UserGroup
					 .DeleteUser(12)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>";

				 result += "<strong>RemoveUser:</strong> " +
					 BaseApp.WSP.UserGroup
					 .RemoveUser(11)
					 .Execute(BaseApp.DB)
					 .ToString() + "<br>"; */
			}
			catch (Exception ex)
			{
				result += "<strong>Error: </strong> " + ex.Message.ToString();
			}
			finally
			{
				Response.Write(result);
			}

			return true;
		}

		public static bool CheckDomain(HttpRequest Request, HttpResponse Response)
		{
			return true;
		}
	}

	#region " Interceptor Test and UIService "

	public class TestUIService : BaseUIService
	{
		public override void BeginInterceptor(string methodName)
		{
			Console.Write("BeginInterceptor");
		}

		public virtual void Algo()
		{
		}
	}

	#endregion " Interceptor Test and UIService "
}