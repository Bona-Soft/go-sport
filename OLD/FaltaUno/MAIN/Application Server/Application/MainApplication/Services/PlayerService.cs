using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using static MYB.FaltaUno.Application.MainApplication.Services.Services;

namespace MYB.FaltaUno.Application.MainApplication.Services
{
   public static class PlayerService
   {
      public static long SavePlayer(IPlayer player)
      {
         Key.SetEmptyObjectToNull(player);

         long result;

         try
         {
            Services.BaseRepoService.BeginTransaction(player);

            result = Services.PlayerRepoService.Save(player);

            if (result == -1)
            {
               BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("CreatePlayer", "PlayerAlreadyExist", "You already have a player for this sport"));
               Services.BaseRepoService.RollbackTransaction(player);
               return -1;
            }
            Services.BaseRepoService.CommitTransaction(player);
         }
         catch (Exception ex)
         {
            Services.BaseRepoService.RollbackTransaction(player);
            throw ex;
         }

         return result;
      }

		public static bool EnablePlayer(long playerID, bool enabled)
		{
			IPlayer player = GetPlayer(playerID);
			player.Active = enabled;
         SavePlayer(player);
         player = GetPlayer(playerID);

         return player.Active;
		}

      public static IPlayer CreateDefaultPlayer(IUser user, ISport sport)
      {
         IPlayer player = Services.PlayerFactory.New();
         player.User = user;
         player.Sport = sport;
         player.Alias = player.User.Name;

         player = CreatePlayer(player);

         return player;
      }

		public static IPlayer CreatePlayer(IPlayer player)
      {
         if (player.Sport == null)
         {
            player.Sport = SportService.GetSelectedSport();
         }
         if (player.User == null)
         {
            player.User = UserService.GetCurrentUser();
         }

         long playerID = SavePlayer(player);

         player.User.Initialized = false;
         player.User = UserService.GetUser(player.User.UserID);


         if (playerID == -1)
         {
            return player.User.Players.Single(x => x.Sport.SportID == player.Sport.SportID);
         }

         return GetPlayer(playerID);
      }

      public static IPlayer EditPlayer(IPlayer player)
      {
         Key.RefillEntity<IPlayer, long>(player, player.PlayerID, GetPlayer);

         SavePlayer(player);
         return GetPlayer(player.PlayerID);
      }

      public static IPlayer GetPlayer(long playerID)
      {
         IPlayer player = Services.PlayerRepoService.Get(playerID);
			player.User = UserService.GetUser(player.User.UserID);
         return player;
      }

      public static IPlayer GetPlayer(IUser user, ISport sport)
      {
         return user.Players.Where(player => player.Sport.SportID == sport.SportID).FirstOrDefault();
      }

      public static List<IPlayer> GetPlayers(IUser user)
      {
         return Services.PlayerRepoService.GetByUserID(user.UserID);
      }

		public static IPlayer GetCurrentPlayer()
		{
			IUser user = UserService.GetCurrentUser();

         return GetPlayer(user, SportService.GetSelectedSport());
		}
      #region "Player Searchs"

      public static List<IPlayer> SearchPlayers(Filter<FPlayerSearch> filter)
      {
         //TODO: contains para filtros de 1 key

         if (UserService.GetCurrentUser() != null)
         {
            if (!filter.ContainsKey("UserID", typeof(long)))
               filter.Add("UserID", typeof(long), UserService.GetCurrentUser().UserID.Value);
         }

			var players = Services.PlayerRepoService.SearchPlayers(filter);

			return players.Select(p => PlayerService.GetPlayer(p.PlayerID)).ToList(); ;
      }

        public static List<IPlayer> GetTopPlayers()
        {
            List<IPlayer> players = Services.PlayerRepoService.GetTopPlayers();
            foreach (IPlayer player in players)
            {
                player.User = UserService.GetUser(player.User.UserID);
            }
            return players;
        }

      public static List<IPlayer> GetFrecuentlyPlayers() 
         => GetFrecuentlyPlayers(UserService.GetCurrentUser(), SportService.GetSelectedSport());

		public static List<IPlayer> GetFrecuentlyPlayers(IUser user)
         => GetFrecuentlyPlayers(user, SportService.GetSelectedSport());
      
         
		public static List<IPlayer> GetFrecuentlyPlayers(IUser user, ISport sport)
      {
         List<IPlayer> players = Services.PlayerRepoService.GetFrecuentlyPlayers(user.UserID, sport.SportID);
			foreach(IPlayer player in players)
			{
				player.User = UserService.GetUser(player.User.UserID);
			}
			return players;
      }

		public static List<IPlayer> GetRecommendedPlayers(IMatch match) => GetRecommendedPlayers(match, UserService.GetCurrentUser());
		public static List<IPlayer> GetRecommendedPlayers(IMatch match, IUser user)
		{
			List<IPlayer> players = Services.PlayerRepoService.GetRecommendedPlayers(user.UserID, match.MatchID);
         foreach (IPlayer player in players)
         {
            player.User = UserService.GetUser(player.User.UserID);
         }
         return players;
		}


      #endregion "Player Searchs"

      public static IPlayer GetPlayer(long userID, short sportID)
      {
         if (userID == 0)
         {
            userID = UserService.GetCurrentUser().UserID;
				if (sportID == 0)
				{
					sportID = UserService.GetCurrentUser().LastActiveSport.SportID;
				}
			}
        

         IPlayer player = Services.PlayerRepoService.GetPlayer(userID, sportID);

         if(player == null)
         {
            BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("Player", "PlayerNotFound", "Player not found"));
            return null;
         }

         player.Fields = GetPlayerFields(player.PlayerID);
         player.PlayerPositions = GetPlayerPositions(player.PlayerID);
         player.ChallengeTypes = GetPlayerChallengeTypes(player.PlayerID);

         return player;
      }

      public static List<IField> GetPlayerFields(long playerID)
      {
         return Services.PlayerRepoService.GetPlayerFields(playerID);
      }

      public static List<IFieldPosition> GetPlayerPositions(long playerID)
      {
         return Services.PlayerRepoService.GetPlayerPositions(playerID);
      }

      public static List<IChallengeType> GetPlayerChallengeTypes(long playerID)
      {
         return Services.PlayerRepoService.GetPlayerChallengeTypes(playerID);
      }

		public static List<IMatchPlayerRequest> GetCurrentUserMatchPlayerRequests()
		{
			return GetMatchPlayerRequests(UserService.GetCurrentUser().UserID);
		}

		public static List<IMatchPlayerRequest> GetMatchPlayerRequests(long userID)
		{

			List<IMatchPlayerRequest> mprs = PlayerRepoService.GetMatchPlayerRequests(userID);
         mprs = mprs.Select(x => MatchService.FillFull(x)).ToList();
         return mprs;

      }
	}
}