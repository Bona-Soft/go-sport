using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{
	public class Team : Entity, ITeam
	{
		public key<int> TeamID { get; private set; }

		public Team(int teamID)
		{
			TeamID = teamID;
		}

      public Team()
      {
         TeamID = 0;
      }


      private Type Flag;
		private Type Shield;
		private Type Uniform;

		private Type Clasification;
		private Type FavoriteField;
		private Type Presentation;

		private string Name;
		private Country Country;
		private List<Type> Colors;
		private IPlayer Captain;

		//public string Name { get; set; }
		//public ISport Sport { get; set; }
		//public IPlayer Captain { get; set; }

		//public ILocation DefaultLocation { get; set; }
		//public IHeadquarter DefaultHeadquarter { get; set; }

		//public bool DefaultFieldsOnly { get; set; }
		//public bool DefaultMatchTypesOnly { get; set; }
		//public bool DefaultChallengeTypesOnly { get; set; }

		//public List<IField> Fields { get; set; }
		//public List<IMatchType> MatchTypes { get; set; }
		//public List<IChallengeType> ChallengeType { get; set; }

		//public short Public { get; set; }
		//public DateTime MatchDateTime { get; set; }
		//public short MaxPlayers { get; set; }
		//public short MinPlayers { get; set; }
		//public bool LimitlessPlayers { get; set; }
		//public int AverageAge { get; set; }
		//public string Comments { get; set; }

		//public List<IPlayer> Players { get; set; }
	}
}