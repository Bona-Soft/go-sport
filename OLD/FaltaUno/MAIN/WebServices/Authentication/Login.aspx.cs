using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;

namespace Authentication
{
	public partial class Login : AppWebServices
	{
		protected override void ProcessGET()
		{
			JObject user = getRequestJObject("user");

			if (!IsLogged)
			{
				string username = getRequestParam("username", String.Empty);
				string password = getRequestParam("password", String.Empty);
				bool remember = getRequestParam("remember", false);
				if (username != String.Empty && password != String.Empty)
					UserUIService.Login(username, password, Request.UserHostAddress, Request.UserAgent, remember);
			}

			if (IsLogged)
			{
				user = UserUIService.GetLoggedUser(user);
				sendSerializeObject(user);
				return;
			}

			ErrorManager.AddError(TranslationManager.Get("UserOrPasswordInvalid"));
			ErrorManager.Verify();
		}
	}
}