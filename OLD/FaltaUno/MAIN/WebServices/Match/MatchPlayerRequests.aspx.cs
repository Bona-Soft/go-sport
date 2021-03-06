using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Match
{
	public partial class MatchPlayerRequests : AppWebServices
	{

		protected override void ProcessPOST()
		{	
         //TODO: llamar data en vez de object al JArray
			if(Data["object"] != null && Data["object"].Type == JTokenType.Array)
			{
				JArray jArr = (JArray)Data["object"];
				
				for(int i = 0; i <jArr.Count -1; i++)
				{
					if(jArr[i] != null && jArr.Type != JTokenType.Null)
					{
						JObject obj = (JObject)jArr[i];
						ErrorManager.MandatoryError<JObject, long>(obj, "Match", "MatchID", TranslationManager.Get("MatchPlayerRequest", "Match.MatchID.Mandatory", "Match not provided"));
						ErrorManager.MandatoryError<JObject, long>(obj, "PlayerReceiver", "PlayerID", TranslationManager.Get("MatchPlayerRequest", "PlayerReceiver.PlayerID", "Player not provided"));

						ErrorManager.Verify();
					}
					else
					{
						jArr.RemoveAt(i);
					}
				}

				jArr = MatchUIService.SendMatchPlayerRequests(jArr);

				ErrorManager.Verify();

				sendSerializeObject(jArr);
			}
			else
			{
				ErrorManager.MandatoryError<JObject, long>(Data, "Match", "MatchID", TranslationManager.Get("MatchPlayerRequest", "Match.MatchID.Mandatory", "Match not provided"));
				ErrorManager.MandatoryError<JObject, long>(Data, "PlayerReceiver", "PlayerID", TranslationManager.Get("MatchPlayerRequest", "PlayerReceiver.PlayerID", "Player not provided"));

				ErrorManager.Verify();

				JObject obj = MatchUIService.SendMatchPlayerRequest(Data);

				ErrorManager.Verify();

				sendSerializeObject(obj);
			}
		}

		protected override void ProcessGET()
		{
			JArray jarr;
			long ID = getRequestParam<long>("MatchID", 0);

			ErrorManager.MandatoryError(ID, TranslationManager.Get("MatchPlayerRequest", "MandatoryMatchID", "MatchID not provided"));
			ErrorManager.Verify();

			jarr = MatchUIService.GetMatchPlayerRequests(ID);
			sendSerializeObject(jarr);
		}
	}
}