using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;

namespace MYB.FaltaUno.Model.Factories
{
	public class TeamFactory : BaseFactory<Team, ITeam>, ITeamFactory
	{
		public ITeam Team(int teamID)
		{
			return new Team(teamID);
		}
	}
}