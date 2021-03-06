using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Infrastructure.Interfaces.Repositories
{
	public interface IMatchRepository : ISingleton
	{
		DataTable Get(long matchID);
		long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		DataTable SearchMatch(Dictionary<string, Type, object> dic);
      DataTable GetMatchesByUser(Dictionary<string, Type, object> dic);
      DataTable GetMatchPlayerRequest(long matchPlayerRequestID);
		DataTable GetMatchPlayerRequests(long matchID);
		long AddMatchPlayerRequest(Dictionary<string, Type, object> dic);
		long UpdateMatchPlayerRequest(Dictionary<string, Type, object> dic);
		DataTable GetMatchTeamRequest(long matchID);
		long AddMatchTeamRequest(Dictionary<string, Type, object> dic);
		long UpdateMatchTeamRequest(Dictionary<string, Type, object> dic);
		DataTable GetMatchPlayerRequestStates();

		DataTable GetChallengeTypes();
      DataTable GetMatchTypes();
      DataTable GetMatchTypes(short sportID);
		DataTable GetMatchStates();
		DataTable GetMatchStates(short matchStateID);

		long RefreshStatusMatches();

		long CloseExpiredMatchPlayerRequests();


	}
}