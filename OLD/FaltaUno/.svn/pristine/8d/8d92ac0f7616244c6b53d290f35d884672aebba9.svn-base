using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IField : IEntity
	{
      key<int> FieldID { get; }

      ISport Sport { get; set; }
		
		List<IFieldPosition> Positions { get; set; }

      string Value { get; set; }
      string Description { get; set; }

      bool Default { get; set; }
   }
}