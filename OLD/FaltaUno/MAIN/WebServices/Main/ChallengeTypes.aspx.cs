using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Main
{
	public partial class ChallengeTypes : AppWebServices
	{

		protected override void ProcessGET()
		{
			sendSerializeObject(MainUIService.GetChallengeTypes());
		}
	}
}