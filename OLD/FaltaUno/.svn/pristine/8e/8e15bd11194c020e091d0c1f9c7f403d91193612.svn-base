using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IMatch : IEntity
	{
		key<long> MatchID { get; }

		string Name { get; set; }
		ISport Sport { get; set; }
		ILocation Location { get; set; }
		IHeadquarter Headquarter { get; set; }
		IField Field { get; set; }
      short Public { get; set; }
      DateTime MatchDateTime { get; set; }
		short MaxPlayers { get; set; }
		short MinPlayers { get; set; }
		bool LimitlessPlayers { get; set; }
		IMatchType MatchType { get; set; }
		IChallengeType ChallengeType { get; set; }
		int AverageAge { get; set; }
		IMatchState MatchState { get; set; }
		IPlayer MatchPlayerOwner { get; set; }
      string Comments { get; set; }

		List<IMatchPlayerRequest> PlayersRequest { get; set; }
		List<IMatchTeamRequest> TeamsRequest { get; set; }
	}
}