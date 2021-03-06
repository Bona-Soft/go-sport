using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MYB.FaltaUno.Application.MainApplication.Services
{
	public static class MatchService
	{
		#region "Match"

		private static long SaveMatch(IMatch match)
		{
			long result = 0;
			Key.SetEmptyObjectToNull(match);

			try
			{
				Services.BaseRepoService.BeginTransaction(match);

				if (match.Location != null)
				{
					if (match.Location.LocationID == 0)
					{
						match.Location = MainService.SaveLocation(match.Location);
					}
				}

				result = Services.MatchRepoService.Save(match);

				//BaseService.MatchRepoService.SaveMatchPlayers(match.Players);

				Services.BaseRepoService.CommitTransaction(match);
			}
			catch (Exception ex)
			{
				try
				{
					Services.BaseRepoService.RollbackTransaction(match);
				}
				catch { }

				throw ex;
			}
			return result;
		}

		public static List<IMatch> FilterMatches(Filter<FMatchSearch> filter)
		{
			List<IMatch> matches = MatchService.SearchMatch(filter);

			if (matches != null && matches.Count > 0)
				FillMatches(matches);

			return matches;
		}

      public static List<IMatch> GetUserMatches(Filter<FUserMatches> filter)
      {             
         List<IMatch> matches = Services.MatchRepoService.GetMatchesByUser(filter);

         if (matches != null && matches.Count > 0)
            FillMatches(matches);

         return matches;
      }

		public static List<IMatch> GetCurrentUserOwnerMatches()
		{
			IPlayer currentPlayer = PlayerService.GetCurrentPlayer();
			return GetCurrentUserMatches().Where(m => m.MatchPlayerOwner.PlayerID == currentPlayer.PlayerID).ToList();
		}


		public static List<IMatch> GetCurrentUserMatches() => GetCurrentUserMatches(new Filter<FUserMatches>());
      public static List<IMatch> GetCurrentUserMatches(Filter<FUserMatches> filter)
		{
         long userID = UserService.GetCurrentUser().UserID;
         filter.Add("UserID", typeof(long), userID);

         return GetUserMatches(filter);

      }

      public static List<IMatch> GetUserMatches(IUser user)
      {
         Filter<FUserMatches> filter = new Filter<FUserMatches>();

         filter.Add("UserID", typeof(long), user.UserID);
         if (!filter.ContainsKey("MatchRequestType", typeof(string)))
         {
            filter.Add("MatchRequestType", typeof(string), RequestStatesTypes.POSITIVE);
         }
            
         if (!filter.ContainsKey("Closed", typeof(bool)))
         {
            filter.Add("Closed", typeof(bool), false);
         }
         

			List<IMatch> matches = Services.MatchRepoService.GetMatchesByUser(filter);

         if (matches != null && matches.Count > 0)
            FillMatches(matches);

         return matches;
      }

		public static List<IMatch> SearchMatch(Filter<FMatchSearch> filter)
		{
         short sportID = SportService.GetSelectedSportID();

         if (sportID != 0)
            filter["SportID"] = sportID;

			List<IMatch> matches = Services.MatchRepoService.SearchMatch(filter);

			return matches;
		}

		public static long UpdateMatch(IMatch match)
		{
			match.MatchState = MatchService.GetMatchState(1);

			try
			{
				BaseApp.BaseRepoService.BeginTransaction("UpdateMatch");

				if (match.Location != null && match.Location.LocationID == 0)
				{
					match.Location.User = UserService.GetCurrentUser();
					match.Location.Sport = UserService.GetActiveSport();
					match.Location = MainService.SaveLocation(match.Location);
				}
				else if (match.Location.User != null && match.Location.User.UserID != UserService.GetCurrentUser().UserID)
				{
					BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("UpdateMatch", "LocationUserNotMatch", "The user of the selected location is not correctly"));
					return -3;
				}

				SaveMatch(match);

				BaseApp.BaseRepoService.CommitTransaction("UpdateMatch");
			}
			catch (Exception ex)
			{
				BaseApp.BaseRepoService.RollbackTransaction("UpdateMatch");
				throw ex;
			}

			return match.MatchID;
		}

		public static long CreateMatch(IMatch match)
		{
			IPlayer matchPlayerOwner = PlayerService.GetPlayer(UserService.GetCurrentUser(), match.Sport);
			if (matchPlayerOwner == null)
			{
				BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("NoPlayerForSelectedSport", "You do not have a configured player for current sport"));
				return -2;
			}

			match.MatchPlayerOwner = matchPlayerOwner;
			match.MatchState = MatchService.GetMatchState(1);

			long matchID = -1;

			try
			{
				BaseApp.BaseRepoService.BeginTransaction("CreateMatch");

				if (match.Location != null && match.Location.LocationID == 0)
				{
					match.Location.User = UserService.GetCurrentUser();
					match.Location.Sport = UserService.GetActiveSport();
					match.Location = MainService.SaveLocation(match.Location);
				}
				if (match.Sport == null || match.Sport.SportID == 0)
				{
					match.Sport = UserService.GetActiveSport();
				}

				matchID = SaveMatch(match);

				SendMatchOwnerPlayerRequest(matchID);

				BaseApp.BaseRepoService.CommitTransaction("CreateMatch");
			}
			catch (Exception ex)
			{
				try
				{
					BaseApp.BaseRepoService.RollbackTransaction("CreateMatch");
				}
				catch { }

				throw ex;
			}

			return matchID;
		}

		public static long EditMatch(IMatch match)
		{
			//Funcion en base que hay que testear,
			// le pasa el match, el id, y la funcion que get de match
			// ejecuta CheckKeys, si necesita hace el getMatch pasandole el ID
			// y luego mergea el match traido por getMatch sobre los nulls de la entidad match

			Key.RefillEntity<IMatch, long>(match, match.MatchID, GetMatch);

			return SaveMatch(match);
		}

		public static IMatch GetFullMatch(long matchID)
		{
			IMatch match = GetMatch(matchID);

			match.MatchPlayerOwner = PlayerService.GetPlayer(match.MatchPlayerOwner.PlayerID);
			match.PlayersRequest = GetMatchPlayerRequests(matchID);
			match.TeamsRequest = Services.MatchRepoService.GetMatchTeamRequest(matchID);

			return match;
		}

		public static IMatch GetMatch(long matchID)
		{
			IMatch match = Services.MatchRepoService.GetByID(matchID);

			match.MatchPlayerOwner = PlayerService.GetPlayer(match.MatchPlayerOwner.PlayerID);
			match.Location = Services.MainRepoService.GetLocation(match.Location.LocationID);

			if (match.Headquarter != null)
			{
				match.Headquarter = Services.HeadquarterRepoService.GetByID(match.Headquarter.HeadquarterID);
			}
			//TODO: Traer objetos completos del subentities manager, no de la DB.
			return match;
		}

		#endregion "Match"

		#region "Match states"

		private static List<IMatchState> _matchStates;

		public static IMatchState GetMatchState(short matchStateID)
		{
			if (_matchStates == null)
			{
				LoadMatchStates();
			}

			return _matchStates.FirstOrDefault(matchState => matchState.MatchStateID == matchStateID);
		}

		public static IMatchState GetMatchState(string value)
		{
			if (_matchStates == null)
			{
				LoadMatchStates();
			}

			return _matchStates.FirstOrDefault(matchState => matchState.Value == value);
		}

		public static void LoadMatchStates()
		{
			_matchStates = Services.MatchRepoService.GetMatchStates();
		}

		public static List<IMatchState> GetMatchStates()
		{
			if (_matchStates == null)
			{
				LoadMatchStates();
			}
			return _matchStates;
		}

		#endregion "Match states"

		#region "Match Types"

		//TODO: hacer lo mmismo que field _fields Load()
		public static List<IMatchType> GetMatchTypes()
		{
			List<IMatchType> matchTypes = Services.MatchRepoService.GetMatchTypes();
			foreach (IMatchType matchType in matchTypes)
			{
				matchType.DefaultField = SportService.GetField(matchType.DefaultField.FieldID);
			}
			return matchTypes;
		}

		public static List<IMatchType> GetMatchTypes(short sportID)
		{
			return Services.MatchRepoService.GetMatchTypes(sportID);
		}

		public static IMatchType GetMatchType(short id)
		{
			IMatchType matchTypes = GetMatchTypes().FirstOrDefault(matchType => matchType.MatchTypeID == id);

			return matchTypes;
		}

		#endregion "Match Types"

		#region "Match Fills"

		public static List<IMatch> FillMatches(List<IMatch> list)
		{
			return list.Select(FillMatch).ToList();
		}

		public static IMatch FillMatch(IMatch match)
		{
			match.ChallengeType = MainService.GetChallengeType(match.ChallengeType.ChallengeTypeID);
			match.Field = SportService.GetField(match.Field.FieldID);
			if (match.Headquarter != null)
				match.Headquarter = HeadquarterService.GetHeadquarter(match.Headquarter.HeadquarterID);
			match.Location = MainService.GetLocation(match.Location.LocationID);
			match.MatchPlayerOwner = PlayerService.GetPlayer(match.MatchPlayerOwner.PlayerID);
			match.MatchState = MatchService.GetMatchState(match.MatchState.MatchStateID);
			match.MatchType = MatchService.GetMatchType(match.MatchType.MatchTypeID);
			match.PlayersRequest = MatchService.GetMatchPlayerRequests(match.MatchID);
			//TODO: match.TeamsRequest =

			return match;
		}

		#endregion "Match Fills"

		#region "Match Requests"

		public static List<IMatchPlayerRequest> GetMatchPlayerRequests(long matchID)
		{
			List<IMatchPlayerRequest> matchPlayerRequests = Services.MatchRepoService.GetMatchPlayerRequests(matchID);
			foreach (IMatchPlayerRequest mpr in matchPlayerRequests)
			{
				Fill(mpr);
			}
			return matchPlayerRequests;
		}

      public static IMatchPlayerRequest GetMatchPlayerRequest(long matchPlayerRequestID)
		{
			IMatchPlayerRequest mpr = Services.MatchRepoService.GetMatchPlayerRequest(matchPlayerRequestID);
			Fill(mpr);
			return mpr;
		}

		public static List<IMatchPlayerRequest> SendMatchPlayerRequests(List<IMatchPlayerRequest> matchPlayerRequests)
		{
			List<IMatchPlayerRequest> mprs = new List<IMatchPlayerRequest>();

			foreach (IMatchPlayerRequest mpr in matchPlayerRequests)
			{
				mprs.Add(SendMatchPlayerRequest(mpr));
			}

			return mprs;
		}

		public static IMatchPlayerRequest SendMatchOwnerPlayerRequest(long matchID)
		{
			IMatchPlayerRequest userOwnerRequest = Services.MatchFactory.NewMatchPlayerRequest();
			userOwnerRequest.PlayerReceiver = PlayerService.GetCurrentPlayer();
			userOwnerRequest.PlayerSender = PlayerService.GetCurrentPlayer();
			userOwnerRequest.Match = Services.MatchFactory.New(matchID);
			userOwnerRequest.MatchPlayerRequestState = MainService.GetRequestState(RequestState.Confirmed);
			return SendMatchPlayerRequest(userOwnerRequest);
		}

		private static class ValidateRequest
		{
			private static class AddErrorMessage
			{
				public static void SamePlayer(string name)
				{
					BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get(
						"SendMatchPlayerRequest",
						"PlayerNotOriginalSenderOrOwner",
						"You are not allowed to send request to this player:")
						+ " " + name);
				}

				public static void MatchOwner()
				{
					BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get(
									"MatchPlayerRequest",
									"MatchOwner",
									"You are not allowed to change this request"));
				}
			}

			public static bool State(IMatchPlayerRequest dbMpr, IMatchPlayerRequest requestMpr)
			{
				if (!ValidateRequest.NotCompleted(dbMpr))
				{
					return false;
				}

				switch ((RequestState)requestMpr.MatchPlayerRequestState.RequestStateID)
				{
					case RequestState.Tentative_not:
					case RequestState.Tentative:
					case RequestState.Tentative_yes:
					case RequestState.Confirmed:
               case RequestState.Confirmed_substitute:
						{
							if (!ValidateRequest.SamePlayer(dbMpr) && !ValidateRequest.MatchOwner(dbMpr))
							{
								if (!ValidateRequest.SamePlayer(dbMpr))
									AddErrorMessage.SamePlayer(dbMpr.PlayerReceiver.User.CompleteName);
								if (!ValidateRequest.MatchOwner(dbMpr))
									AddErrorMessage.MatchOwner();
								return false;
							}
							break;
						}
					case RequestState.Rejected:
					case RequestState.Proposal:
						{
							if (!ValidateRequest.SamePlayer(dbMpr))
							{
								AddErrorMessage.SamePlayer(dbMpr.PlayerReceiver.User.CompleteName);
								return false;
							}
							break;
						}
					case RequestState.Cancelled:
						{
							if (!ValidateRequest.MatchOwner(dbMpr))
							{
								AddErrorMessage.MatchOwner();
								return false;
							}
							break;
						}
					default:
						break;
				}

				return true;
			}

			


			public static bool SamePlayer(IMatchPlayerRequest mpr)
			{
				return mpr.PlayerReceiver.PlayerID == PlayerService.GetCurrentPlayer().PlayerID;
			}

			public static bool NotCompleted(IMatchPlayerRequest mpr)
			{
				if (mpr.MatchPlayerRequestState.RequestStateID == RequestState.Completed)
				{
					BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get(
							"SendMatchPlayerRequest",
							"StateClosed",
							"You can not modfied this request anymore"));
					return false;
				}
				return true;
			}

			public static bool MatchOwner(IMatchPlayerRequest mpr)
			{
				return (mpr.Match.MatchPlayerOwner.PlayerID == PlayerService.GetCurrentPlayer().PlayerID);
			}
		}

      public static IMatchPlayerRequest SaveMatchPlayerRequest(IMatchPlayerRequest mpr)
      {
         try
         {
            BaseApp.BaseRepoService.BeginTransaction("SaveMatchPlayerRequest");

            if (mpr.Comment != null
               && !(mpr.Comment.CommentID > 0)
               && !String.IsNullOrEmpty(mpr.Comment.Text))
            {
               mpr.Comment.User = UserService.GetCurrentUser();
               mpr.Comment = MainService.SaveComment(mpr.Comment);
            }

            long _resultID = Services.MatchRepoService.SaveMatchPlayerRequest(mpr);
            
            BaseApp.BaseRepoService.CommitTransaction("SaveMatchPlayerRequest");

            return GetMatchPlayerRequest(_resultID);
         }
         catch (Exception ex)
         {
            try
            {
               BaseApp.BaseRepoService.RollbackTransaction("SaveMatchPlayerRequest");
            }
            catch { }

            throw ex;
         }
         
      }

		private static bool MatchPlayerRequestCompletedExists(IMatchPlayerRequest matchPlayerRequest, List<IMatchPlayerRequest> matchPlayerRequests)
		{
			bool result = matchPlayerRequests.Exists(
						x => x.PlayerReceiver.PlayerID == matchPlayerRequest.PlayerReceiver.PlayerID
						&& (x.MatchPlayerRequestState.Type == RequestStatesTypes.CLOSED
							|| x.MatchPlayerRequestState.RequestStateID == RequestState.Completed));
			if (result)
			{
				BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get(
					"MatchPlayerRequest",
					"PlayerRequestAlreadyExists",
					"You are unable to send this request. The request already exists."));
			}
			return result;
		}

		public static IMatchPlayerRequest SendMatchPlayerRequest(IMatchPlayerRequest matchPlayerRequest)
		{
			IMatchPlayerRequest mpr;

         try
         {
            BaseApp.BaseRepoService.BeginTransaction("SendMatchPlayerRequest");
         
            if (matchPlayerRequest.MatchPlayerRequestID == 0)
			   {
					List<IMatchPlayerRequest> matchPlayerRequests = MatchService.GetMatchPlayerRequests(matchPlayerRequest.Match.MatchID);

					if (MatchPlayerRequestCompletedExists(matchPlayerRequest, matchPlayerRequests))
					{
						return matchPlayerRequest;
					}

					mpr = matchPlayerRequests.FirstOrDefault(x => x.PlayerReceiver.PlayerID == matchPlayerRequest.PlayerReceiver.PlayerID);

					matchPlayerRequest.PlayerSender = PlayerService.GetPlayer(UserService.GetCurrentUser(), MatchService.GetMatch(matchPlayerRequest.Match.MatchID).Sport);

					if (mpr != null)
					{
						mpr.FillFrom(matchPlayerRequest);
					}
					else
					{
						mpr = matchPlayerRequest;
					}

					if (mpr.MatchPlayerRequestState == null)
					{
						mpr.MatchPlayerRequestState = Services.MatchFactory.NewRequestState(RequestState.Pending);
					}
				}
			   else
			   {
				   mpr = GetMatchPlayerRequest(matchPlayerRequest.MatchPlayerRequestID);
               if(mpr.MatchPlayerRequestState.RequestStateID.Value == matchPlayerRequest.MatchPlayerRequestState.RequestStateID.Value){
                  return mpr;
               }

               mpr.Match = GetFullMatch(mpr.Match.MatchID);

				   if (!ValidateRequest.State(mpr, matchPlayerRequest))
				   {
					   return mpr;
				   }

               if(matchPlayerRequest.MatchPlayerRequestState.RequestStateID == RequestState.Confirmed && mpr.Match.MaxPlayers <= mpr.Match.PlayersRequest.Where(x=> x.MatchPlayerRequestState.RequestStateID == RequestState.Confirmed).ToList().Count)
               {
                  matchPlayerRequest.MatchPlayerRequestState = MainService.GetRequestState(RequestState.Confirmed_substitute);
               }


               ResendPlayerSubtitutesRequest(mpr, matchPlayerRequest);

               mpr.FillFrom(matchPlayerRequest);
			   }

            IMatchPlayerRequest resultMpr = SaveMatchPlayerRequest(mpr);

            if (resultMpr.PlayerReceiver.PlayerID != PlayerService.GetCurrentPlayer().PlayerID)
            {
               resultMpr.PlayerReceiver.User.Base.News++;
            }	

            BaseApp.BaseRepoService.CommitTransaction("SendMatchPlayerRequest");

            return resultMpr;
         }
         catch (Exception ex)
         {
            try
            {
               BaseApp.BaseRepoService.RollbackTransaction("SendMatchPlayerRequest");
            }
            catch { }

            throw ex;
         }
         //TODO: send email, notification o cualquier medio que este registrado
      }

      private static bool ResendPlayerSubtitutesRequest(IMatchPlayerRequest originalMpr,IMatchPlayerRequest newMpr)
      {
         IMatchPlayerRequest _SubstituteToReconfirm(IMatchPlayerRequest mpr)
         {
            mpr.MatchPlayerRequestState = MainService.GetRequestState(RequestState.Reconfirmation_required);
            return mpr;
         }

         //Reenviar solicitudes a los suplentes
         if (originalMpr.MatchPlayerRequestState.RequestStateID == RequestState.Confirmed
            && newMpr.MatchPlayerRequestState.RequestStateID != RequestState.Confirmed
            && originalMpr.Match.MaxPlayers > originalMpr.Match.PlayersRequest.Where(x => x.MatchPlayerRequestState.RequestStateID == RequestState.Confirmed).ToList().Count - 1)
         {
            List<IMatchPlayerRequest> _mprs = 
               originalMpr.Match.PlayersRequest
               .Where(x => x.MatchPlayerRequestState.RequestStateID == RequestState.Confirmed_substitute)
               .Select(x => _SubstituteToReconfirm(x)).ToList();

            foreach (IMatchPlayerRequest auxMpr in _mprs)
            {
               SaveMatchPlayerRequest(auxMpr);
            }
            
            return true;
         }
         return false;
      }

		public static IMatchPlayerRequest Fill(IMatchPlayerRequest mpr)
		{
			if (mpr == null)
				return null;

			mpr.Match = MatchService.GetMatch(mpr.Match.MatchID);
         
         mpr = _Fill(mpr);

         return mpr;
		}

      public static IMatchPlayerRequest FillFull(IMatchPlayerRequest mpr)
      {
         if (mpr == null)
            return null;

         mpr.Match = MatchService.GetFullMatch(mpr.Match.MatchID);

         mpr = _Fill(mpr);

         return mpr;
      }

      private static IMatchPlayerRequest _Fill(IMatchPlayerRequest mpr)
      {
         mpr.MatchPlayerRequestState = MainService.GetRequestState(mpr.MatchPlayerRequestState.RequestStateID);
         mpr.PlayerReceiver = PlayerService.GetPlayer(mpr.PlayerReceiver.PlayerID);
         mpr.PlayerReceiver.User = UserService.GetUser(mpr.PlayerReceiver.User.UserID);

         mpr.PlayerSender = PlayerService.GetPlayer(mpr.PlayerSender.PlayerID);
         mpr.PlayerSender.User = UserService.GetUser(mpr.PlayerSender.User.UserID);

         return mpr;
      }

      #endregion "Match Requests"
   }
}