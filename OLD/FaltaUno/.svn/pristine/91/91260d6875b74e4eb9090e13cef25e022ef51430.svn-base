using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MYB.FaltaUno.WebServices.Player
{
   public partial class PlayerSearch : AppWebServices
   {

      protected override void ProcessGET(Filter filter)
      {
			if(filter.Count == 0)
			{
				ErrorManager.AddError(TranslationManager.Get("Players", "Player.PlayerSearch", "ANYFILTERMANDATORY", "You must select at least one filter"));
			}
         ErrorManager.Verify();

         JArray obj = PlayerUIService.SearchPlayers(filter, GetRequestView());
         sendSerializeObject(obj);

      }
   }
}