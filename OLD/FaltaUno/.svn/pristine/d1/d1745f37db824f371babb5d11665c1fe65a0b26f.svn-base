using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Main
{
	public partial class UserActiveSport : AppWebServices
	{
		protected override void ProcessPOST()
		{
			short sportID = Data["SportID"].ToDefType<short>();
			ErrorManager.MandatoryError(sportID, TranslationManager.Get("Main","Sport.Mandatory", "Sport is not selected"));
			ErrorManager.IsNotLoggedError(TranslationManager.Get("Main", "You are not logged to the system"));
			ErrorManager.Verify();

			UserUIService.SetActiveSport(sportID);

		}

		protected override void ProcessGET()
		{
			JObject obj;

			ErrorManager.IsNotLoggedError(TranslationManager.Get("Main", "You are not logged to the system"));
			ErrorManager.Verify();

			obj = UserUIService.GetActiveSport();
			sendSerializeObject(obj);
		}
	}
}