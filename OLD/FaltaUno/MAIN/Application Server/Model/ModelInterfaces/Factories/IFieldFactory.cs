using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;

namespace MYB.FaltaUno.Model.Interfaces.Factories
{
	public interface IFieldFactory : IBaseFactory<IField>, ISingleton
	{
		IFieldPosition NewFieldPosition(int fieldPositionID);
	}
}