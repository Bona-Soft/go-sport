using Castle.Windsor;
using MYB.BaseApplication.Infrastructure.Windsor;
using System;

namespace MYB.BaseApplication.Application.CoreApplication.AppInits
{
	public class BaseHttpApplication : System.Web.HttpApplication, IContainerAccessor
	{
		//PagesCollection PublicPages;
		//SecuritySection WebModule;

		// protected BaseHttpApplication()
		//{
		//Creates the WebModule for the proper application
		//WebModule = Activator.CreateInstance(System.Type.GetType(SecuritySettings.GetSecuritySection().Application));
		//Get the pages which do not require as session
		//PublicPages = SecuritySettings.GetPublicPagesSection().Pages;
		//}

		protected void Application_Start(object sender, EventArgs e)
		{
			BaseApp.Initializate(new ContainerManager());
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			//string Page;
			//HttpApplication App;
			//HttpRequest Request;
			//SessionManagement SessionMgr;
			//AuthenticationStatus Authe;
			//AuthorizationStatus Author;
			//
			//App = sender as HttpApplication;
			//Request = App.Request;
			//Page = Request.Path;
			//Page = Page.Replace('/', '\\');
			//// Page = ExtractFileName(Page); encontrar la manera de extraer el file name
			//SessionMgr = new SessionManagement(HttpContext.Current);
			//
			////Only in this place the Authentication Status is calculated
			//Authe = SessionMgr.GetAuthenticationStatus();
			//Author = new AuthorizationStatus();
			////Proof of concept Authorization
			//Author.Authorized = ((Authe.Status == 0) && !IsPublicPage(Page)) || IsPublicPage(Page);
			//
			//HttpContext.Current.Items["AuthenticationStatus"] = Authe;
			//HttpContext.Current.Items["AuthorizationStatus"] = Author;
			//
			////if FWebModule.ExecutePage(Page, Context.Current) then App.CompleteRequest;
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

		private bool IsPublicPage(string page)
		{
			return true;
			//for ( int index = 0; PublicPages.count; idnex++)
			//{
			//	if (PublicPages[index].Name.ToUpper() == Page.ToUpper())
			//		return true;
			//}
			//return false;
		}

		#region IContainerAccessor Members

		public IWindsorContainer Container
		{
			get { return ContainerManager.WindsorContainer; }
		}

		#endregion IContainerAccessor Members
	}
}