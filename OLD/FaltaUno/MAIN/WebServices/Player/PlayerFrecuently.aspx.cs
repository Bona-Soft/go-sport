using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Player
{
	public partial class PlayerFrecuently : AppWebServices
	{
		protected override void ProcessGET()
		{
			sendSerializeObject(PlayerUIService.GetFrecuentlyPlayers());
		}
	}
}