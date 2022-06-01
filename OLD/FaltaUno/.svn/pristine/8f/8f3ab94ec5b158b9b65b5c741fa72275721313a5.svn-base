using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class Player : AppWebServices
	{
		protected override void ProcessPOST()
		{
			JObject joPlayer = Data;

			joPlayer = PlayerUIService.CreatePlayer(joPlayer);

			sendSerializeObject(joPlayer);
		}

		protected override void ProcessGET()
		{
			JObject obj;

			long playerID = getRequestParam<long>("playerID", 0);

			BaseApp.MemLogManager.D(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

			if (playerID > 0)
			{
				obj = PlayerUIService.GetPlayer(playerID, GetRequestView());
				
			}
			else
			{
				long userID = getRequestParam<long>("UserID", 0);
				short sportID = getRequestParam<short>("SportID", 0);

				obj = PlayerUIService.GetPlayer(userID, sportID);
			}
         ErrorManager.Verify();


			sendSerializeObject(obj);

		}
	}
}