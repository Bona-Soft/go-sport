using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;

namespace MYB.FaltaUno.Model.Factories
{
	public class HeadquarterFactory : BaseFactory<Headquarter, IHeadquarter>, IHeadquarterFactory
	{
		public IHeadquarter Headquarter(int headquarterID)
		{
			return new Headquarter(headquarterID);
		}
	}
}