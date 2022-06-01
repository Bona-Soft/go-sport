using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MYB.FaltaUno.WebServices.Main
{
	public partial class Sport : AppWebServices
	{

		protected override void ProcessGET()
		{
			short sportID = getRequestParam<short>("SportID", 0);

			if (sportID != 0)
			{
				sendSerializeObject(MainUIService.GetSport(sportID));
			}
			else
			{
				sendSerializeObject(MainUIService.GetSports());
			}
		}
	}
}