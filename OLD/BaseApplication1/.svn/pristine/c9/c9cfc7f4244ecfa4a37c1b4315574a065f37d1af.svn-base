using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using System.Net.Http;
using System.Web;

namespace MYB.BaseApplication.UserInterfaces.VirtualPages
{
	public class CreateUser : IVirtualPage
	{
		public void Register()
		{
			BaseApp.VirtualPageManager.RegisterPage("CreateUser.aspx", HttpMethod.Post, Post);
		}

		public static bool Post(HttpRequest Request, HttpResponse Response)
		{
			long UserID = BaseApp.BaseUserManager.CreateUser(Request["username"], Request["password"]);
			Response.Write("Usuario creado: " + UserID.ToString());
			return true;
		}
	}
}