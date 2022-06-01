using MYB.BaseApplication.Framework.Helpers;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IHeadquarter : IEntity
	{
		key<int> HeadquarterID { get; }

		List<ISport> Sport { get; set; }

		ILocation Location { get; set; }

		string Name { get; set; }
	}
}