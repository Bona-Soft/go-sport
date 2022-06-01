using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using System.Net.Http;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.DeployManager
{
	public class DeployManagerPage : IVirtualPage
	{
		public void Register()
		{
			BaseApp.VirtualPageManager.RegisterPage("DeployManager.aspx", HttpMethod.Post, DeployPage);
		}

		public static bool DeployPage(HttpRequest Request, HttpResponse Response)
		{
			//DeployManager.ReconfigWebConfig();

			return true;
		}
	}
}