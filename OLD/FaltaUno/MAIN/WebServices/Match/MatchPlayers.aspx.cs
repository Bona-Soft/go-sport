using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Match
{
	public partial class MatchPlayers : AppWebServices
	{
		protected override void ProcessPOST(JArray data)
		{
			ErrorManager.MandatoryError(data, TranslationManager.Get("MatchPlayers", "MatchPlayersMandatoryData", "The request can not be sended"));
			ErrorManager.MinCountError(data, 1, TranslationManager.Get("MatchPlayers", "MatchPlayersMandatoryData", "The request can not be sended"));
			ErrorManager.Verify();

			MatchUIService.SendMatchPlayerRequests(data);

		}

		protected override void ProcessGET()
		{
			JArray jarr;
			long ID = getRequestParam<long>("MatchID", 0);

			ErrorManager.MandatoryError(ID, TranslationManager.Get("MatchID", "MatchIDMandatory", "Match not provided"));
			ErrorManager.Verify();

			jarr = MatchUIService.GetMatchPlayerRequests(ID);
			sendSerializeObject(jarr);
		}
	}
}