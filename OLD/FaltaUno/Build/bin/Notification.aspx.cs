using MYB.FaltaUno.Application.UIService;

namespace MYB.FaltaUno.WebServices.Notification
{
   public partial class Notification : AppWebServices
	{
		protected override void ProcessPOST()
		{
			
			//string str = Data["param"].ToDefString();
			//JObject obj = (JObject)Data["JsonObj"];

			//ErrorManager.MandatoryError(str, TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.MandatoryError<JObject>(Data, "JsonObj", TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.Verify();

			//UIService.CreateEditUpdate(str, obj);

		}

		protected override void ProcessGET()
		{
			//JObject obj;
			//long ID = getRequestParam<long>("ID", 0);

			//ErrorManager.MandatoryError(ID, TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.Verify();

			//obj = UIService.Get(ID);
			//sendSerializeObject(obj);
		}
	}
}