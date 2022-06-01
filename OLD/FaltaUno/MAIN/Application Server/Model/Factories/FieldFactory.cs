using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;

namespace MYB.FaltaUno.Model.Factories
{
	public class FieldFactory : BaseFactory<Field, IField>, IFieldFactory
	{
		public IFieldPosition NewFieldPosition(int fieldPositionID)
		{
			return new FieldPosition(fieldPositionID);
		}
	}
}