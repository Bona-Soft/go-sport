using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{

	public class MatchRepoService : RepoServices, IMatchRepositoryService
	{
		#region "Match"
		public long Save(IMatch match)
		{
			Dictionary<string, Type, object> dic = Key.EntityToDictionary(match);
			dic.Add("RegisterUserID", typeof(long), CurrentUserID);

			if (match.MatchID > 0)
				return MatchRepo.Update(dic, BaseRepoService.CurrentTransaction);
			dic.Remove<long>("MatchID");
			return MatchRepo.Add(dic, BaseRepoService.CurrentTransaction);
		}

		public IMatch GetByID(long matchID)
		{
			DataTable dt = MatchRepo.Get(matchID);
			if (dt == null || dt.Rows.Count == 0)
				throw new Exception("Match can not be found");

			IMatch match = MatchFactory.New(matchID);
			Fill(match, dt);
			return match;
		}

		public List<IMatch> SearchMatch(Filter<FMatchSearch> filter)
		{
			return Key.GetEntityList<IMatch, long, Filter<FMatchSearch>>(filter,
				MatchRepo.SearchMatch,
				MatchFactory.New,
				Fill,
				"MatchID");
		}

		public List<IMatch> GetMatchesByUser(Filter<FUserMatches> filter)
		{
			DataTable dt = MatchRepo.GetMatchesByUser(filter);
			List<IMatch> matches = new List<IMatch>();
			if (dt == null || dt.Rows.Count == 0)
				return matches;

			foreach (DataRow row in dt.Rows)
			{
				IMatch match = MatchFactory.New(row["MatchID"]);
				Fill(match, row);
				matches.Add(match);
			}
					
			return matches;
		}

		public void RefreshStatusMatches()
		{
			MatchRepo.RefreshStatusMatches();
		}
		
		#endregion

		#region "MatchPlayerRequest"

		public void CloseExpiredMatchPlayerRequests()
		{
			MatchRepo.CloseExpiredMatchPlayerRequests();
		}

		public IMatchPlayerRequest GetMatchPlayerRequest(long matchPlayerRequest)
		{
			return Key.GetEntity<IMatchPlayerRequest,long,long>(
				matchPlayerRequest,
				MatchRepo.GetMatchPlayerRequest,
				MatchFactory.NewMatchPlayerRequest,
				Fill,
				"MatchPlayerRequestID");
		}

		public List<IMatchPlayerRequest> GetMatchPlayerRequests(long matchID)
		{
			return Key.GetEntityList<IMatchPlayerRequest, long, long>(matchID,
				MatchRepo.GetMatchPlayerRequests,
				MatchFactory.NewMatchPlayerRequest,
				Fill,
				"MatchPlayerRequestID");
		}

		public long SaveMatchPlayerRequest(IMatchPlayerRequest matchPlayerRequest)
		{
			Dictionary<string, Type, object> dic = Key.EntityToDictionary(matchPlayerRequest);
							
			dic.Add("PlayerRequestReceiverID", typeof(long), matchPlayerRequest.PlayerReceiver.PlayerID.Value);
			dic.Add("PlayerRequestSenderID", typeof(long), matchPlayerRequest.PlayerSender.PlayerID.Value);
			dic.Add("MatchPlayerRequestStateID", typeof(short), matchPlayerRequest.MatchPlayerRequestState.RequestStateID.Value);

			if (matchPlayerRequest.MatchPlayerRequestID > 0)
			{
            MatchRepo.UpdateMatchPlayerRequest(dic);
            return matchPlayerRequest.MatchPlayerRequestID;
         }
				
			dic.Remove<long>("MatchPlayerRequestID");
			return MatchRepo.AddMatchPlayerRequest(dic);

		}

		public List<IMatchTeamRequest> GetMatchTeamRequest(long matchID)
		{
			return Key.GetEntityList<IMatchTeamRequest, long, long>(matchID,
				MatchRepo.GetMatchTeamRequest,
				MatchFactory.NewMatchTeamRequest,
				Fill,
				"MatchTeamRequestID");
		}

		public IMatchTeamRequest SaveMatchTeamRequest(IMatchTeamRequest matchTeamRequest)
		{
			Dictionary<string, Type, object> dic = Key.EntityToDictionary(matchTeamRequest);

			dic.Add("PlayerRequestReceiverID", typeof(long), matchTeamRequest.PlayerRequestReceiver.PlayerID);
			dic.Add("PlayerRequestSenderID", typeof(long), matchTeamRequest.PlayerRequestSender.PlayerID);

			if (matchTeamRequest.MatchTeamRequestID > 0)
			{
				long result = MatchRepo.UpdateMatchTeamRequest(dic);
				if (result > 0)
					return matchTeamRequest;
				return null;
			}

			dic.Remove<long>("MatchPlayerRequestID");
			dic.Add("MatchPlayerRequestStateID", typeof(short), 1);
			long id = MatchRepo.AddMatchTeamRequest(dic);
			IMatchTeamRequest mpr = MatchFactory.NewMatchTeamRequest(id);
			mpr.FillFrom(matchTeamRequest);

			return mpr;
		}

		public List<IRequestStates> GetRequestStates()
		{
         List<IRequestStates> list = new List<IRequestStates>();
         DataTable dt = MatchRepo.GetMatchPlayerRequestStates();
         if (dt == null || dt.Rows.Count == 0)
            return list;

         foreach (DataRow row in dt.Rows)
         {
            IRequestStates entity = MatchFactory.NewRequestState((RequestState)row["MatchPlayerRequestStateID"].ToDefType<int>());
            Key.FillEntity(entity, row);
            list.Add(entity);
         }

         return list;
		}

		#endregion "MatchPlayerRequest"

		#region "Entity List Gets"

		public List<IChallengeType> GetChallegeTypes()
		{
			return Key.GetEntityList<IChallengeType, short>(
				MatchRepo.GetChallengeTypes,
				MatchFactory.NewChallengeType,
				Fill,
				"ChallengeTypeID");
		}

		

		public List<IMatchType> GetMatchTypes()
		{
			return Key.GetEntityList<IMatchType, short>(
				MatchRepo.GetMatchTypes,
				MatchFactory.NewMatchType,
				MatchRepoService.Fill,
				"MatchTypeID");
		}

		public List<IMatchType> GetMatchTypes(short sportID)
		{
			return Key.GetEntityList<IMatchType, short, short>(sportID,
				MatchRepo.GetMatchTypes,
				MatchFactory.NewMatchType,
				MatchRepoService.Fill,
				"MatchTypeID");
		}

		public List<IMatchState> GetMatchStates()
		{
			return Key.GetEntityList<IMatchState, short>(
				MatchRepo.GetMatchStates,
				MatchFactory.NewMatchState,
				Key.FillEntity,
				"MatchStateID");
		}

		public List<IMatchState> GetMatchStates(short matchStateID)
		{
			return Key.GetEntityList<IMatchState, short, short>(matchStateID,
				MatchRepo.GetMatchStates,
				MatchFactory.NewMatchState,
				Key.FillEntity,
				"MatchStateID");
		}

		#endregion "Entity List Gets"

		#region "Fills Entity Methods"

		private IMatch Fill(IMatch match, DataTable dt)
			=> Fill(match, dt.Rows[0]);

		public IMatch Fill(IMatch match, DataRow dr)
		{
			//TODO: revisar si hace falta cargar cada uno de las entidades (ej Field) o si el fill entity lo hace solo.
			//TODO: Revisar si hace falta cargar sport
			Key.FillEntity(match, dr);
			match.Field = FieldFactory.NewDef(dr["FieldID"], null);
			match.Location = LocationFactory.NewDef(dr["LocationID"], null);
			match.Headquarter = HeadquarterFactory.NewDef<int>(dr["HeadquarterID"], null);
			match.MatchType = dr["MatchTypeID"] == DBNull.Value ? null : MatchFactory.NewMatchType(Convert.ToInt16(dr["MatchTypeID"]));
			match.ChallengeType = dr["ChallengeTypeID"] == DBNull.Value ? null : MatchFactory.NewChallengeType(Convert.ToInt16(dr["ChallengeTypeID"]));
			match.MatchState = dr["MatchStateID"] == DBNull.Value ? null : MatchFactory.NewMatchState(Convert.ToInt16(dr["MatchStateID"]));
			match.MatchPlayerOwner = PlayerFactory.NewDef(dr["PlayerID"], null);
         match.Sport = SportFactory.NewDef(dr["SportID"], null);

			return match;
		}

		public IChallengeType Fill(IChallengeType challengeType, DataRow dr)
		{
			Key.FillEntity(challengeType, dr);
			return challengeType;
		}

		public IMatchType Fill(IMatchType matchType, DataRow dr)
		{
			Key.FillEntity(matchType, dr);
			matchType.Sport = SportFactory.NewDef(dr["SportID"], null);
			matchType.DefaultField = FieldFactory.NewDef(dr["DefaultFieldID"], null);

			return matchType;
		}

		public IMatchPlayerRequest Fill(IMatchPlayerRequest entity, DataRow dr)
		{
			Key.FillEntity(entity, dr);
			entity.Match = MatchFactory.New(dr["MatchID"]);
			entity.PlayerSender = PlayerFactory.NewDef(dr["PlayerRequestSenderID"], null);
			entity.PlayerReceiver = PlayerFactory.NewDef(dr["PlayerRequestReceiverID"], null);
			entity.MatchPlayerRequestState = MatchFactory.Resolve<IRequestStates, long>(dr["MatchPlayerRequestStateID"].ToDefType<long>());
			entity.Comment = MainFactory.Resolve<IComment, long>(dr["CommentID"].ToDefType<long>());

			return entity;
		}

		public IMatchTeamRequest Fill(IMatchTeamRequest entity, DataRow dr)
		{
			Key.FillEntity(entity, dr);

			return entity;
		}

		#endregion "Fills Entity Methods"
	}
}