using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class PlayerProfile : AppWebServices
	{
		protected override void ProcessPOST()
		{
			//string str;

			//str = Data["param"].ToDefString();

			//ErrorManager.MandatoryError(str, TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.Verify();

			//UIService.CreateEditUpdate(str);

		}

		protected override void ProcessGET()
		{
			JObject obj;
			long userID = getRequestParam<long>("UserID", 0);
			short sportID = getRequestParam<short>("SportID", 0);

			obj = PlayerUIService.GetPlayer(userID,sportID);
			sendSerializeObject(obj);
		}
	}
}