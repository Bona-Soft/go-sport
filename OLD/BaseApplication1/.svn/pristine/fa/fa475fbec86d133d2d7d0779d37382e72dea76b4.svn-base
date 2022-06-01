using System;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseWebController : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string filePath = String.Empty;

			filePath = Request.Url.Query.Substring(1);

			if (BaseApp.VirtualPageManager.ExecutePage(filePath, Request, Response))
			{
				Response.End();
			}
			Server.Execute(virtualRootPath + filePath);
			Response.End();
		}

		private string virtualRootPath
		{
			get
			{
				if (Request.ApplicationPath == "/")
				{
					return Request.ApplicationPath;
				}

				return Request.ApplicationPath + "/";
			}
		}
	}
}