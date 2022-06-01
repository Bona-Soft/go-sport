using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class UserPersonalInfo : AppWebServices
	{
		protected override void ProcessPOST()
		{

			ErrorManager.MandatoryError<long>(Data, "UserID", TranslationManager.Get("UserPersonalInfo", "UserMandatory", "User date not provided"));
			ErrorManager.MandatoryError<DateTime>(Data, "BirthDate", TranslationManager.Get("UserPersonalInfo", "BirthDateMandatory", "Birth date not provided"));
			ErrorManager.MandatoryError<string>(Data, "Name", TranslationManager.Get("UserPersonalInfo", "NameMandatory", "Name date not provided"));
			ErrorManager.MandatoryError<string>(Data, "LastName", TranslationManager.Get("UserPersonalInfo", "LastNameMandatory", "LastName date not provided"));
			ErrorManager.Verify();

			JObject obj = UserUIService.EditPersonalInfo(Data);

			ErrorManager.Verify();

			sendSerializeObject(obj);
		}
	}
}