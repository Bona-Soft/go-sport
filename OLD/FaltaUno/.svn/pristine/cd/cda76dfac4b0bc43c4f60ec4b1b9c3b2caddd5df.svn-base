using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.Application.MainApplication
{
	public static class MailManager
	{
		public static void SendUserVerificationEmail(string emailAddress, string verificationCode)
		{
			BaseApp.MailManager.Mail.Subject = BaseApp.TranslationManager.Get("VerifyEmailTitle");
			BaseApp.MailManager.Mail.Body = 
				"http://" + BaseApp.BaseConfigManager().UrlHost
				+ "/#/" + BaseApp.GeneralParameters.Get<string>("VerifyUserPage", "VerifyUser") 
				+ "/" + emailAddress 
				+ "/" + verificationCode;
			BaseApp.MailManager.Mail.AddTo(emailAddress);
			BaseApp.MailManager.Send();

			if(BaseApp.MailManager.LastException != null)
			{
				BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("MailManager","FailureSendingEmail", "There was a problem sending the verification code to your email"));
				BaseApp.MailManager.LastException = null;
			}

		}
	}
}