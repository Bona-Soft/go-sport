using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Headquarter
{
	public partial class Headquarter : AppWebServices
	{
		protected override void ProcessGET()
		{
			long headquarterID = getRequestParam<long>("HeadquertarID", 0);
			if (headquarterID != 0)
			{
				//GetJObject
			}
			else
			{
				string searchText = getRequestParam("searchText", "");
				float lat = getRequestParam<float>("Lat", 0);
				float lng = getRequestParam<float>("Lng", 0);

				JArray obj = HeadquarterUIService.SearchHeadquartes(searchText, lat, lng);
				sendSerializeObject(obj);
			}
		}
	}
}