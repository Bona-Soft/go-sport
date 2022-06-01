using MYB.FaltaUno.Application.MainApplication;
using MYB.FaltaUno.Application.UIService;

namespace MYB.FaltaUno.WebServices.Authentication
{
	public partial class Logout : AppWebServices
	{
		protected override void ProcessPOST()
		{
			AppSession.Logout();
		}
	}
}