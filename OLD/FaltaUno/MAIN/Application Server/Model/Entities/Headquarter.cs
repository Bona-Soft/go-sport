using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{
	public class Headquarter : Entity, IHeadquarter
	{
		public key<int> HeadquarterID { get; private set; }

		public List<ISport> Sport { get; set; }

		public ILocation Location { get; set; }
		
		public string Name { get; set; }

		public Headquarter(int id)
		{
			HeadquarterID = id;
		}

      public Headquarter()
      {
         HeadquarterID = 0;
         Sport = new List<ISport>();
      }
   }
}