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
    public partial class TopPlayer : AppWebServices
    {

        protected override void ProcessGET()
        {
            JArray obj = PlayerUIService.GetTopPlayers();

            ErrorManager.Verify();

            sendSerializeObject(obj);

        }
    }
}