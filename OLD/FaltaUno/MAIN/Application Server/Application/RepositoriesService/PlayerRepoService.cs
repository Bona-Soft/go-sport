using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{
   public class PlayerRepoService : RepoServices, IPlayerRepositoryService
   {
      public long Save(IPlayer player)
      {
         Dictionary<string, Type, object> dic = Key.EntityToDictionary(player);
         if (!dic.ContainsKey("UserID", typeof(long)))
            dic.Add("UserID", typeof(long), player.User.UserID);

         if (player.PlayerID > 0)
            return PlayerRepo.Update(dic, BaseRepoService.CurrentTransaction);
         dic.Remove<long>("PlayerID");
         return PlayerRepo.Add(dic, BaseRepoService.CurrentTransaction);
      }

      public IPlayer Get(long playerID)
      {
         DataTable dt = PlayerRepo.Get(playerID);
         if (dt == null || dt.Rows.Count == 0)
            throw new Exception("Player can not be found");

         IPlayer player = PlayerFactory.New(playerID);
         Fill(player, dt.Rows[0]);
         return player;
      }

		public IPlayer GetPlayer(long userID, short sportID)
		{
         DataTable dt = PlayerRepo.Get(userID,sportID);
         if (dt == null || dt.Rows.Count == 0)
            return null;

         IPlayer player = PlayerFactory.New(dt.Rows[0]["PlayerID"]);
         Fill(player, dt.Rows[0]);
         return player;
      }

		public List<IPlayer> GetByUserID(long userID)
      {
         return Key.GetEntityList<IPlayer, long, long>(
            userID,
            PlayerRepo.GetAllByUserID,
            PlayerFactory.New,
            Fill,
            "PlayerID");
      }

      public List<IPlayer> SearchPlayers(Filter<FPlayerSearch> fPlayer)
      {
         List<IPlayer> players = new List<IPlayer>();

         DataTable dt = PlayerRepo.SearchPlayers(fPlayer);
			I($"Result count: {dt.Rows.Count}");

			foreach (DataRow row in dt.Rows)
         {
				IPlayer player = PlayerFactory.New(row["PlayerID"]);
            Fill(player, row);
            players.Add(player);
         }
         return players;
      }

        public List<IPlayer> GetTopPlayers()
        {
            return Key.GetEntityList<IPlayer, long>(
              PlayerRepo.GetTopPlayers,
              PlayerFactory.New,
              PlayerRepoService.Fill,
              "PlayerID");
        }

        public List<IPlayer> GetFrecuentlyPlayers(long userID, short sportID)
		{
			List<IPlayer> players = new List<IPlayer>();
			DataTable dt = PlayerRepo.GetFrecuentlyPlayers(userID, sportID);
			foreach (DataRow row in dt.Rows)
			{
				IPlayer player = PlayerFactory.New(row["PlayerID"]);
				Fill(player, row);
				players.Add(player);
			}
			return players;
		}

		public List<IPlayer> GetRecommendedPlayers(long userID, long matchID)
		{
			List<IPlayer> players = new List<IPlayer>();
			DataTable dt = PlayerRepo.GetRecommendedPlayers(userID, matchID);
			foreach (DataRow row in dt.Rows)
			{
				IPlayer player = PlayerFactory.New(row["PlayerID"]);
				Fill(player, row);
				players.Add(player);
			}
			return players;
		}

		public List<IField> GetPlayerFields(long playerID)
		{
			List<IField> fields = new List<IField>();
			DataTable dt = PlayerRepo.GetPlayerFields(playerID);

			foreach (DataRow row in dt.Rows)
			{
				IField field = FieldFactory.New(row["FieldID"]);
				FieldRepoService.Fill(field, row);
				fields.Add(field);
			}
			return fields;
		}

		public List<IFieldPosition> GetPlayerPositions(long playerID)
		{
			List<IFieldPosition> fieldPositions = new List<IFieldPosition>();
			DataTable dt = PlayerRepo.GetPlayerPositions(playerID);

			foreach (DataRow row in dt.Rows)
			{
				IFieldPosition fieldPosition = FieldFactory.NewFieldPosition(row["FieldPositionID"].ToDefType<int>());
				FieldRepoService.Fill(fieldPosition, row);
				fieldPositions.Add(fieldPosition);
			}
			return fieldPositions;
		}

		public List<IChallengeType> GetPlayerChallengeTypes(long playerID)
		{
			return Key.GetEntityList<IChallengeType, short, long>(
			  playerID,
			  PlayerRepo.GetPlayerChallengeTypes,
			  MatchFactory.NewChallengeType,
			  MatchRepoService.Fill,
			  "ChallengeTypeID");
		}

		public List<IMatchPlayerRequest> GetMatchPlayerRequests(long userID)
		{
			return Key.GetEntityList<IMatchPlayerRequest, long, long>(
			  userID,
			  PlayerRepo.GetMatchPlayerRequestsByUser,
			  MatchFactory.NewMatchPlayerRequest,
			  MatchRepoService.Fill,
			  "MatchPlayerRequestID");
		}

		#region "Fills Entity Methods"

		public IPlayer Fill(IPlayer player, DataRow dr)
      {
         Key.FillEntity(player, dr);
         player.Sport = SportFactory.NewDef(Convert.ToInt16(dr["SportID"]), null);
         player.User = UserFactory.User(Convert.ToInt64(dr["UserID"]));
			
         return player;
      }

		

		#endregion "Fills Entity Methods"
	}
}