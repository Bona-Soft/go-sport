using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{
	public class TeamRepoService : RepoServices, ITeamRepositoryService
	{
		public long Save(ITeam team)
		{
			Dictionary<string, Type, object> dic = Key.EntityToDictionary(team);
			//dic.Add("Captain", typeof(long), team);

			if (team.TeamID > 0)
				return TeamRepo.Update(dic, BaseRepoService.CurrentTransaction);
			dic.Remove<long>("TeamID");
			return TeamRepo.Add(dic, BaseRepoService.CurrentTransaction);
		}

		public ITeam GetByID(long teamID)
		{
			//TODO: Matchrepo
			DataTable dt = PlayerRepo.Get(teamID);
			if (dt == null || dt.Rows.Count == 0)
				throw new Exception("Match can not be found");

			ITeam team = TeamFactory.New(teamID);
			Fill(team, dt.Rows[0]);
			return team;
		}

      #region "Fills Entity Methods"

		public ITeam Fill(ITeam team, DataRow dr)
		{
			Key.FillEntity(team, dr);

			return team;
		}

      #endregion
   }
}