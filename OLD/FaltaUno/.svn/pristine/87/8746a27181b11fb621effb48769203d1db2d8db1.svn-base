using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class UserNews : AppWebServices
	{
		//protected override void ProcessPOST()
		//{
		//	string str = Data["param"].ToDefString();
		//	JObject obj = (JObject)Data["JsonObj"];

		//	ErrorManager.MandatoryError(str, TranslationManager.Get("module", "constanteName", "Your default message"));
		//	ErrorManager.MandatoryError<JObject>(Data, "JsonObj", TranslationManager.Get("module", "constanteName", "Your default message"));
		//	ErrorManager.Verify();

		//	UIService.CreateEditUpdate(str, obj);

		//}

		protected override void ProcessGET()
		{
			JArray arr;
			//long ID = getRequestParam<long>("PAge", 0);

			//ErrorManager.MandatoryError(ID, TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.Verify();

			arr = MainUIService.GetUserNews();
			sendSerializeObject(arr);
		}
	}
}