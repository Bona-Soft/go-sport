using Castle.Windsor;
using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Infrastructure.Windsor;
using System;
using System.IO;

namespace BaseApplicationTestPage
{
	public class Global : System.Web.HttpApplication, IContainerAccessor
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			BaseApp.Initializate(new ContainerManager());
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
		{
		}

		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			if (BaseApp.VirtualPageManager.ExecutePage(Path.GetFileName(Request.PhysicalPath), Request, Response))
			{
				CompleteRequest();
			}
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

		#region IContainerAccessor Members

		public IWindsorContainer Container
		{
			get { return ContainerManager.WindsorContainer; }
		}

		#endregion IContainerAccessor Members
	}
}