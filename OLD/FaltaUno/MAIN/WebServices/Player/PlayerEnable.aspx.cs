using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class PlayerEnable : AppWebServices
	{
		protected override void ProcessPUT()
		{

			ErrorManager.MandatoryError(Data["PlayerID"], TranslationManager.Get("PlayerEnable", "PlayerIDMandatory", "PlayerID not supplied"));
			ErrorManager.MandatoryError(Data["Active"], TranslationManager.Get("PlayerEnable", "ActiveMandatory", "State not supplied"));
			ErrorManager.MandatoryError(Data["SportID"], TranslationManager.Get("PlayerEnable", "SportMandatory", "Sport not selected"));

			ErrorManager.Verify();

			JObject obj = PlayerUIService.EnablePlayer(Data["PlayerID"].ToDefType<long>(), Data["Active"].ToDefType<bool>());
			sendSerializeObject(obj);
		}

	}
}