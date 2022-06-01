using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System.Web;
using System;
using System.Threading;

namespace MYB.FaltaUno.WebServices.Main
{
	public partial class Locations : AppWebServices
	{
		protected override void ProcessPOST()
		{
			//string str;

			//str = Data["param"].ToDefString();

			//ErrorManager.MandatoryError(str, TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.Verify();

			//UIService.CreateEditUpdate(str);

		}

		protected override void ProcessGET()
		{
			int groupNumber = getRequestParam("groupNumber", 0);
			string searchText = getRequestParam("searchText","");
			sendSerializeObject(MainUIService.GetLocations(searchText, groupNumber));
		}
	}
}