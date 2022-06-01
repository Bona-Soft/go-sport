using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Application.Interfaces.RepositoriesService
{
	public interface ITeamRepositoryService : IPerWebRequest
	{
		long Save(ITeam team);

		ITeam GetByID(long teamID);

		ITeam Fill(ITeam team, DataRow dr);
	}
}