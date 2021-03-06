using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Model.Repositories
{
	public class PlayerRepository : IPlayerRepository
	{
		public long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("Player", dbParams.ToArray(), trx);
		}

		public long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			throw new NotImplementedException();
		}

		public DataTable Get(long playerID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("PlayerID", playerID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetPlayer", dbParams.ToArray());
		}

      public DataTable Get(long userID, short sportID)
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();

         dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));
         dbParams.Add(BaseApp.DB.CreateParameter("SportID", sportID, OleDbType.TinyInt));

         return BaseApp.DB.Execute<DataTable>("FindPlayer", dbParams.ToArray());
      }

      public DataTable GetAllByUserID(long userID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetPlayersByUserID", dbParams.ToArray());
		}

		public DataTable Filter(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Execute<DataTable>("FindMatch", dbParams.ToArray());
		}

		public long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<long>("Player", dbParams.ToArray(), trx);
		}

      public DataTable SearchPlayers(Dictionary<string, Type, object> dic)
      {
         List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

         return BaseApp.DB.Execute<DataTable>("SearchPlayers", dbParams.ToArray());
      }

		public DataTable GetFrecuentlyPlayers(long userID, short sportID)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));
			dbParams.Add(BaseApp.DB.CreateParameter("SportID", sportID, OleDbType.TinyInt));

			return BaseApp.DB.Execute<DataTable>("GetFrecuentlyPlayers", dbParams.ToArray());
		}
		public DataTable GetRecommendedPlayers(long userID, long matchID)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));
			dbParams.Add(BaseApp.DB.CreateParameter("MatchID", matchID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetRecommendedPlayers", dbParams.ToArray());
		}

        public DataTable GetTopPlayers()
        {
            return BaseApp.DB.Execute<DataTable>("GetTopPlayers");
        }


        public DataTable GetPlayerFields(long playerID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();
			dbParams.Add(BaseApp.DB.CreateParameter("PlayerID", playerID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetPlayerFields", dbParams.ToArray());
		}

		public DataTable GetPlayerPositions(long playerID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();
			dbParams.Add(BaseApp.DB.CreateParameter("PlayerID", playerID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetPlayerPositions", dbParams.ToArray());
		}

		public DataTable GetMatchPlayerRequestsByUser(long userID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();
			dbParams.Add(BaseApp.DB.CreateParameter("UserID", userID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetMatchPlayerRequestsByUserID", dbParams.ToArray());
		}

		public DataTable GetPlayerChallengeTypes(long playerID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();
			dbParams.Add(BaseApp.DB.CreateParameter("PlayerID", playerID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetPlayerChallengeTypes", dbParams.ToArray());
		}
	}
}