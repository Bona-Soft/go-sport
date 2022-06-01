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
	public partial class UserMatchPlayerRequests : AppWebServices
	{
		protected override void ProcessGET()
		{
			JArray jarr = PlayerUIService.GetCurrentUserMatchPlayerRequests();
			sendSerializeObject(jarr);
		}
	}
}