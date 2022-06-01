using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;

namespace MYB.FaltaUno.Model.Interfaces.Factories
{
	public interface IMatchFactory : IBaseFactory<IMatch>, ISingleton
	{
		IMatchType NewMatchType(short matchTypeID);
		IMatchState NewMatchState(short matchStateID);
      IChallengeType NewChallengeType(short challengeTypeID);
		IMatchPlayerRequest NewMatchPlayerRequest(long id);
      IMatchPlayerRequest NewMatchPlayerRequest();

      IMatchTeamRequest NewMatchTeamRequest(long id);
		IRequestStates NewRequestState(RequestState id);
   }
}