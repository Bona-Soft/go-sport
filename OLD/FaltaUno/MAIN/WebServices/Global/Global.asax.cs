using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Infrastructure.Windsor;
using MYB.FaltaUno.Application.MainApplication.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using System.Web.Routing;

namespace MYB.FaltaUno.WebServices.Global
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			BaseApp.Initializate(new ContainerManager());
			JobService.ScheduleJobs();
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			//if (BaseApp.VirtualPageManager.ExecutePage(Path.GetFileName(Request.PhysicalPath), Request, Response))
			//{
			//   CompleteRequest();
			//}
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{
		}

		protected void Application_End(object sender, EventArgs e)
		{
		}
	}
}