using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Infrastructure.Interfaces.Repositories
{
	public interface IPlayerRepository : ISingleton
	{
		DataTable Get(long playerID);
      DataTable Get(long userID, short sportID);
      DataTable GetAllByUserID(long userID);
		long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);

      DataTable SearchPlayers(Dictionary<string, Type, object> dic);
		DataTable GetFrecuentlyPlayers(long userID, short sportID);
		DataTable GetRecommendedPlayers(long userID, long matchID);
      DataTable GetTopPlayers();

      DataTable GetPlayerFields(long playerID);
		DataTable GetPlayerPositions(long playerID);
		DataTable GetPlayerChallengeTypes(long playerID);

		DataTable GetMatchPlayerRequestsByUser(long userID);
		DataTable Filter(Dictionary<string, Type, object> dic);
	}
}