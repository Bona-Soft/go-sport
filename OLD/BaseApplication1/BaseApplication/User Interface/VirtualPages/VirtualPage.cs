using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using System.Net.Http;
using System.Web;

namespace MYB.BaseApplication.UserInterfaces.VirtualPages
{
	public class VirtualPage : IVirtualPage
	{
		public void Register()
		{
			BaseApp.VirtualPageManager.RegisterPage("VirtualPage.aspx", HttpMethod.Post, Post);
		}

		public static bool Post(HttpRequest Request, HttpResponse Response)
		{
			Response.Write("Funciona mierda");
			return true;
		}
	}
}