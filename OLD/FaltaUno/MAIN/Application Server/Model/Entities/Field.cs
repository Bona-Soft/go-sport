using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{
	//Futbol: Indistinto, Cesped, Sintetico, Parque //Tennis: Cemento, Ladrillo, Cesped
	public class Field : Entity, IField
	{
		
		public key<int> FieldID { get; private set; }

      public ISport Sport { get; set; }

		public List<IFieldPosition> Positions { get; set; }

		public string Value{ get; set; }
      public string Description { get; set; }
      public bool Default { get; set; }

      public Field(int fieldID)
		{
			FieldID = fieldID;
		}

		public Field()
		{
		}
	}
}