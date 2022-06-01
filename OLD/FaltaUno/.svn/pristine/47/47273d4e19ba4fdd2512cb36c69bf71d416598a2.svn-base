using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreApplication.BaseExtensions;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.WebServices.Match
{
   public partial class Match : AppWebServices
   {
      protected override void ProcessPOST()
      {
         JObject joMatch = Data;
         
         ValidateRequestData(joMatch);
         ErrorManager.Verify();

         joMatch = MatchUIService.CreateMatch(joMatch);

         BaseApp.ErrorManager.Verify();

         sendSerializeObject(joMatch);
      }

      private void ValidateRequestData(JObject jObj)
      {
         ErrorManager.MandatoryError<string>(jObj, "Name", TranslationManager.Get("Match.Name.Mandatory", "Match name can not be empty"));
         ErrorManager.MandatoryError<JObject>(jObj, "LocationGroup", TranslationManager.Get("Match.Location.Mandatory", "Match location can not be empty"));
         ErrorManager.MandatoryError<DateTime>(jObj, "MatchDateTime", TranslationManager.Get("Match.Date.Mandatory", "Match date can not be empty"));
         ErrorManager.MandatoryError<JObject, short>(jObj, "Sport", "SportID", TranslationManager.Get("Match.Sport.Mandatory", "Sport is not selected"));
         ErrorManager.MandatoryError<JObject, int>(jObj, "Field", "FieldID", TranslationManager.Get("Match.Field.Mandatory", "Field is not selected"));
         ErrorManager.MandatoryError<JObject, short>(jObj, "MatchType", "MatchTypeID", TranslationManager.Get("Match.MatchType.Mandatory", "The match type is not selected"));
         ErrorManager.MandatoryError<JObject, short>(jObj, "ChallengeType", "ChallengeTypeID", TranslationManager.Get("Match.ChallengeType.Mandatory", "The challenge type is not selected"));

      }

      protected override void ProcessPUT()
      {
         JObject joMatch = Data;

         ValidateRequestData(joMatch);

         ErrorManager.MandatoryError<long>(joMatch, "MatchID", TranslationManager.Get("UpdateMatch", "MatchIDMandatory", "There was an error when saving match."));
         ErrorManager.Verify();

         joMatch = MatchUIService.UpdateMatch(joMatch);
         ErrorManager.Verify();

         sendSerializeObject(joMatch);
      }

      protected override void ProcessGET()
		{
         JObject obj;
         long matchID = getRequestParam<long>("MatchID", 0);
			ErrorManager.MandatoryError(matchID, TranslationManager.Get("Match","MatchIDMandatory","Match not provided"));
			ErrorManager.Verify();

         UIView uiView = GetRequestView();
         if (uiView == UIView.Custom1)
         {
            obj = MatchUIService.GetCompleteMatch(matchID);
         }
         else
         {
            obj = MatchUIService.GetMatch(matchID, uiView);
         }
			
			sendSerializeObject(obj);

		}

		
	}
}