using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreApplication.BaseExtensions;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.WebServices.Debug
{
   public partial class Log : AppWebServices
   {
    
      protected override void ProcessGET()
		{
			if (!BaseApp.GeneralParameters.Get("DebugModeEnabled", true))
			{
				ErrorManager.AddError("Not available");
			}
			ErrorManager.Verify();

			int matchID = getRequestParam<int>("capacity", 1000);
			string level = getRequestParam<string>("level", "None");

			JArray arr = DebugUIService.GetMemLog(matchID, level);
			
			sendSerializeObject(arr);

		}

		
	}
}