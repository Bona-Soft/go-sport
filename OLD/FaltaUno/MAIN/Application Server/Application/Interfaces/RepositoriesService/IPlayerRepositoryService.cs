using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.Interfaces.RepositoriesService
{
	public interface IPlayerRepositoryService : IPerWebRequest
	{
		long Save(IPlayer player);

		IPlayer Get(long playerID);
		IPlayer GetPlayer(long userID, short sportID);
      List<IPlayer> GetByUserID(long userID);

      //TODO: crear FSearch y que los search sean hijos de este FSearch
      List<IPlayer> SearchPlayers(Filter<FPlayerSearch> palyerFilter);
		List<IPlayer> GetFrecuentlyPlayers(long userID, short sportID);
		List<IPlayer> GetRecommendedPlayers(long userID, long matchID);

        List<IPlayer> GetTopPlayers();

        List<IField> GetPlayerFields(long playerID);
      List<IFieldPosition> GetPlayerPositions(long playerID);
      List<IChallengeType> GetPlayerChallengeTypes(long playerID);

		List<IMatchPlayerRequest> GetMatchPlayerRequests(long userID);
		IPlayer Fill(IPlayer player, DataRow dr);

		
	}
}