using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class ChangePassword : AppWebServices
	{
		protected override void ProcessPOST()
		{
			string currentPassword = Data["currentPassword"].ToDefString();
			string newPassword = Data["newPassword"].ToDefString();
			string repeatPassword = Data["repeatPassword"].ToDefString();


			ErrorManager.MandatoryError(currentPassword, TranslationManager.Get("ChangePassword", "CurrentPasswordMandatory", "Current password not provided"));
			ErrorManager.MandatoryError(newPassword, TranslationManager.Get("ChangePassword", "NewPasswordMandatory", "New password not provided"));
			ErrorManager.MandatoryError(repeatPassword, TranslationManager.Get("ChangePassword", "RepeatPasswordMandatory", "Repeat password not provided"));

			if(newPassword != repeatPassword)
			{
				ErrorManager.AddError(TranslationManager.Get("ChangePassword", "PasswordsNotMatch", "Your repeat password does not match with your new password"));
			}
			ErrorManager.Verify();


			if(UserUIService.ChangeUserPassword(currentPassword, newPassword))
			{
				SendSerializeMessage(TranslationManager.Get("ChangePassword", "PasswordChangeSuccessfully", "Your password was changed successfully"));
			}
			else
			{
				ErrorManager.Verify();
				SendSerializeErrorMessage(TranslationManager.Get("ChangePassword", "PasswordChangeFail", "there was a problem changing your password"));
			}
		}
	}
}