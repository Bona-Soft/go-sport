using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{
	public class Sport : Entity, ISport
	{
		public key<short> SportID { get; private set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public List<IFieldPosition> PlayerPositions { get; set; }
		public List<IField> Fields { get; set; }
      public List<IMatchType> MatchTypes { get; set; }

      public Sport()
      {
         SportID = 0;
      }
      public Sport(short sportID)
		{
			SportID = sportID;
		}
	}
}