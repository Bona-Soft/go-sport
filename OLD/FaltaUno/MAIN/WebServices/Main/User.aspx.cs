using MYB.BaseApplication.Application.CoreApplication;
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
	public partial class User : AppWebServices
	{
		protected override void ProcessPOST()
		{

		}

		protected override void ProcessGET()
		{
			long userID = getRequestParam<long>("UserID", 0);

			JObject obj;
			if (userID == 0)
			{
				obj = UserUIService.GetLoggedUser();
			}
			else
			{
				//TODO: Permisos para ver que datos de usuario etc
				obj = UserUIService.GetUser(userID);
			}
			sendSerializeObject(obj);

		}
	}
}