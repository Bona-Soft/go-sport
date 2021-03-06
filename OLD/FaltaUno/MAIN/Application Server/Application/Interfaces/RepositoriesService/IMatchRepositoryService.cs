using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.Interfaces.RepositoriesService
{
   public interface IMatchRepositoryService : IPerWebRequest
   {
      long Save(IMatch match);
		IMatch GetByID(long matchID);
		List<IMatch> SearchMatch(Filter<FMatchSearch> filter);
		List<IMatch> GetMatchesByUser(Filter<FUserMatches> filter);

		void RefreshStatusMatches();
		void CloseExpiredMatchPlayerRequests();

		long SaveMatchPlayerRequest(IMatchPlayerRequest matchPlayerRequest);
		List<IMatchPlayerRequest> GetMatchPlayerRequests(long matchID);
		IMatchPlayerRequest GetMatchPlayerRequest(long matchPlayerRequest);
		IMatchTeamRequest SaveMatchTeamRequest(IMatchTeamRequest matchPlayerRequest);
		List<IMatchTeamRequest> GetMatchTeamRequest(long matchID);
		List<IRequestStates> GetRequestStates();

		List<IChallengeType> GetChallegeTypes();
      List<IMatchType> GetMatchTypes();
      List<IMatchType> GetMatchTypes(short sportID);
		List<IMatchState> GetMatchStates();
		List<IMatchState> GetMatchStates(short matchStateID);
		
      IMatch Fill(IMatch match, DataRow dr);
      IMatchType Fill(IMatchType matchType, DataRow dr);
      IChallengeType Fill(IChallengeType challengeType, DataRow dr);

		IMatchPlayerRequest Fill(IMatchPlayerRequest matchPlayerRequest, DataRow dr);
   }
}