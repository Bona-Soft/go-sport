using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Model.Repositories
{
	public class MatchRepository : IMatchRepository
	{
		#region "Match"

		public long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("Match", dbParams.ToArray(), trx);
		}

		public long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			throw new NotImplementedException();
		}

		public DataTable Get(long matchID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("@MatchID", matchID, OleDbType.BigInt));

			return BaseApp.DB.Execute<DataTable>("GetMatch", dbParams.ToArray());
		}

		public long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<long>("Match", dbParams.ToArray(), trx);
		}

		public DataTable SearchMatch(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Execute<DataTable>("SearchMatch", dbParams.ToArray());
		}

		public DataTable GetMatchesByUser(Dictionary<string, Type, object> dic)
		{
         List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

         return BaseApp.DB.Execute<DataTable>("GetMatchesByUser", dbParams.ToArray());
		}

		#endregion "Match"

		#region "Match Request"

		public DataTable GetMatchPlayerRequest(long matchPlayerRequestID)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("MatchPlayerRequestID", matchPlayerRequestID, OleDbType.BigInt));

			return BaseApp.DB.Get<DataTable>("MatchPlayerRequest", dbParams.ToArray());
		}

		public DataTable GetMatchPlayerRequests(long matchID)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("MatchID", matchID, OleDbType.BigInt));

			return BaseApp.DB.Get<DataTable>("MatchPlayerRequests", dbParams.ToArray());
		}

		public long AddMatchPlayerRequest(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("MatchPlayerRequest", dbParams.ToArray());
		}

		public long UpdateMatchPlayerRequest(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<long>("MatchPlayerRequest", dbParams.ToArray());
		}

		public DataTable GetMatchTeamRequest(long matchID)
		{
			List<OleDbParameter> dbParams;

			dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("@MatchID", matchID, OleDbType.BigInt));

			return BaseApp.DB.Get<DataTable>("MatchTeamRequest", dbParams.ToArray());
		}

		public long AddMatchTeamRequest(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Add<long>("MatchTeamRequest", dbParams.ToArray());
		}

		public long UpdateMatchTeamRequest(Dictionary<string, Type, object> dic)
		{
			List<OleDbParameter> dbParams = BaseApp.DB.CreateParameter(dic);

			return BaseApp.DB.Update<long>("MatchTeamRequest", dbParams.ToArray());
		}

		public DataTable GetMatchPlayerRequestStates()
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();
			return BaseApp.DB.Execute<DataTable>("GetMatchPlayerRequestStates", dbParams.ToArray());
		}
		#endregion "Match Request"

		public long RefreshStatusMatches()
		{
			return BaseApp.DB.Execute<long>("JobRefreshStatusMatches");
		}

		public long CloseExpiredMatchPlayerRequests()
		{
			return BaseApp.DB.Execute<long>("JobCloseExpiredMatchPlayerRequests");
		}

		public DataTable GetChallengeTypes()
		{
			return BaseApp.DB.Execute<DataTable>("GetChallengeTypes");
		}

		public DataTable GetMatchTypes()
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();
			return BaseApp.DB.Execute<DataTable>("GetMatchTypes", dbParams.ToArray());
		}

		public DataTable GetMatchTypes(short sportID)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("@SportID", sportID, OleDbType.TinyInt));

			return BaseApp.DB.Execute<DataTable>("GetMatchTypes", dbParams.ToArray());
		}

		public DataTable GetMatchStates()
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();
			return BaseApp.DB.Execute<DataTable>("GetMatchStates", dbParams.ToArray());
		}

		public DataTable GetMatchStates(short matchStateID)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>();

			dbParams.Add(BaseApp.DB.CreateParameter("@MatchStateID", matchStateID, OleDbType.TinyInt));

			return BaseApp.DB.Execute<DataTable>("GetMatchStates", dbParams.ToArray());
		}

		
	}
}