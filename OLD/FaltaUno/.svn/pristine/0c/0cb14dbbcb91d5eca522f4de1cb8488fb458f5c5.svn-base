using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Collections.Generic;
using MYB.FaltaUno.Model.Interfaces.SubEntities;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface ISport : IEntity
	{
		key<short> SportID { get; }
		string Name { get; set; }
		string Value { get; set; }		

      List<IField> Fields { get; set; }
      List<IMatchType> MatchTypes { get; set; }
   }
}