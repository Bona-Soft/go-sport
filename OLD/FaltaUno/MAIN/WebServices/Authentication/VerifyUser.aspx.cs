using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Application.MainApplication;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;

namespace MYB.FaltaUno.WebServices.Authentication
{
	public partial class VerifyUser : AppWebServices
	{
		protected override void ProcessGET()
		{
			//BaseApp.BaseUserManager.Create("mariano", "1234");
			string emailAddress = getRequestParam("email", "");
			string verificationCode = getRequestParam("code", "");

			short result = UserUIService.VerifyUser(emailAddress, verificationCode);
			if (result == -1)
			{
				ErrorManager.AddError(TranslationManager.Get("UserAddressNotRegister"), 1);
			}
			else if (result == -2)
			{
				ErrorManager.AddError(TranslationManager.Get("UserAlreadyRegister"), 2);
			}
			else if (result == -3)
			{
				ErrorManager.AddError(TranslationManager.Get("VerificationCodeNotMatch"), 3);
			}
			ErrorManager.Verify();

			JObject jo = new JObject();
			jo["result"] = true;
			sendSerializeObject(jo);
		}
	}
}