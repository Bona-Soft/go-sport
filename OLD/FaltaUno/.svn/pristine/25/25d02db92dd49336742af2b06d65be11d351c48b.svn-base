using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class UserPrivacy : AppWebServices
	{
		protected override void ProcessPUT()
		{
			JObject obj = UserUIService.EditPrivacy(Data);
			ErrorManager.Verify();
         sendSerializeObject(obj);
		}

		protected override void ProcessGET()
		{
			JObject obj = UserUIService.GetCurrentUserPrivacy();
			ErrorManager.Verify();
			sendSerializeObject(obj);
		}
	}
}