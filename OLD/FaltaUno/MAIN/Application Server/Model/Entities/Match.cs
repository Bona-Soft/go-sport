using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Entities.SubEntities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{
	public class Match : Entity, IMatch
	{
		public key<long> MatchID { get; private set; }

		public string Name { get; set; }
		public ISport Sport { get; set; }
		public IPlayer MatchPlayerOwner { get; set; }
		
		public ILocation Location { get; set; }
		public IHeadquarter Headquarter { get; set; }
		public IField Field { get; set; } //Futbol: Indistinto, Cesped, Sintetico, Parque //Tennis: Cemento, Ladrillo, Cesped
		public IMatchType MatchType { get; set; } //Futbol: Futsall, Salon, De 5, de 7, de 11 //Tennis: Single, Dobles, Otro
		public IChallengeType ChallengeType { get; set; } //Todos: Amistoso, Torneo, Copa, Profesional 
		public IMatchState MatchState { get; set; }

      public short Public { get; set; }
      public DateTime MatchDateTime { get; set; }
		public short MaxPlayers { get; set; }
		public short MinPlayers { get; set; }
		public bool LimitlessPlayers { get; set; }
		public int AverageAge { get; set; }
      public string Comments{ get; set; }

		public List<IMatchPlayerRequest> PlayersRequest { get; set; }
		public List<IMatchTeamRequest> TeamsRequest { get; set; }

		public Match(long matchID)
		{
			MatchID = matchID;
		}

		public Match()
		{
			MatchID = 0;
			MatchDateTime = DateTime.Now;

			PlayersRequest = new List<IMatchPlayerRequest>();
			TeamsRequest = new List<IMatchTeamRequest>();
		}
	}
}