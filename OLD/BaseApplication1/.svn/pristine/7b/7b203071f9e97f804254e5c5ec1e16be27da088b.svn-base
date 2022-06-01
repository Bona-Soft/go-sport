using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Cryptography;
using System;
using System.Web;

namespace MYB.BaseApplication.Infrastructure.TestApp
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string result = "Estaba logueado: " + BaseApp.Session().IsLogged.ToString() + "<br><br>";

			string hash = HashHelper.Create("hola");
			result += "<br>Hash: " + hash;
			result += "<br>Hash match: " + HashHelper.Verify("hola", hash);
			result += "<br>Hash match second form: " + HashHelper.Verify("hola", "hola".ToHash());
			//Llamar baseApp.Init

			if (Request["logout"] != null && Request["logout"].ToString() == "1")
			{
				BaseApp.Session().Logout();

				result += "LOGOUT";
				result += " <br><br>DESPUES:: TieneCookieBaseAppCreada: " + (HttpContext.Current.Request.Cookies["BaseAppSession"] != null).ToString();
				result += " <br>DESPUES:: TieneSessionDeBrowser " + HttpContext.Current.Session["BaseAppSession"] != null;
				result += " <br>DESPUES:: estaLogueado " + BaseApp.Session().IsLogged.ToString();
				result += "<br>Session: " + BaseApp.Session().SessionString;
				if (BaseApp.Session().User != null)
				{
					result += "<br>Usuario Actual: " + BaseApp.Session().User.ToString();
					result += "<br>UserId: " + BaseApp.Session().User.UserID.ToString();
				}
			}
			else if (Request["LoginName"] != null && Request["LoginName"].ToString() != string.Empty)
			{
				result += "<br>URL Host: " + BaseApp.BaseConfigManager().UrlHost;

				result += "<br>Existe username " + Request["LoginName"].ToString() + ": " + BaseApp.Session().ValidateUserName(Request["LoginName"].ToString()).ToString();

				BaseApp.Session().Login(Request["LoginName"].ToString(), Request["LoginPassword"].ToString(), Request.UserHostAddress);

				result += " <br>LOGIN";
				result += " <br><br>DESPUES:: TieneCookieBaseAppCreada: " + (HttpContext.Current.Request.Cookies["BaseAppSession"] != null).ToString();
				if (HttpContext.Current.Session["BaseAppSession"] != null)
				{
					result += " <br>DESPUES:: SessionDeBrowser " + HttpContext.Current.Session["BaseAppSession"].ToString();
				}
				result += "<br>Session: " + BaseApp.Session().SessionString;
				if (BaseApp.Session().User != null)
				{
					result += "<br>Usuario Actual: " + BaseApp.Session().User.ToString();
					result += "<br>UserId: " + BaseApp.Session().User.UserID.ToString();
				}
			}

			result += "<br><br>Esta logueado: " + BaseApp.Session().IsLogged.ToString() + "<br><br>";
			//result += " <br> " + BaseApp.Configuration.physicalAppDir;
			//result += " <br> " + BaseApp.Configuration.urlHost[0];

			Response.Write(result);
		}
	}
}