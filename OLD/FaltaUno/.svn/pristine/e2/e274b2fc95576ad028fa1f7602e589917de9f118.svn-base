using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class PlayerRecommended : AppWebServices
	{
		protected override void ProcessGET()
		{
			long ID = getRequestParam<long>("MatchID", 0);

			ErrorManager.MandatoryError(ID, TranslationManager.Get("PlayerREcommended", "MatchIDMandatory", "Match not supplied"));
			ErrorManager.Verify();

			sendSerializeObject(PlayerUIService.GetRecommendedPlayers(ID));
		}
	}
}