using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;

namespace MYB.FaltaUno.WebServices.Main
{
	public partial class Fields : AppWebServices
	{

		protected override void ProcessGET()
		{
			short fieldID = getRequestParam<short>("FieldID", 0);

			if (fieldID != 0)
			{
				sendSerializeObject(MainUIService.GetField(fieldID));
			}
			else
			{
				sendSerializeObject(MainUIService.GetFields());
			}
		}
	}
}