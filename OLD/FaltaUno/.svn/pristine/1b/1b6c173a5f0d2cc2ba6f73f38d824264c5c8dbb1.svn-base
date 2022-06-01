using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Entities;
using MYB.FaltaUno.Model.Entities.SubEntities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;
using MYB.FaltaUno.Model.Interfaces.SubEntities;

namespace MYB.FaltaUno.Model.Factories
{
	public class MatchFactory : BaseFactory<Match, IMatch>, IMatchFactory
	{
		public IMatchType NewMatchType(short matchTypeID)
		{
			return new MatchType(matchTypeID);
		}
		public IChallengeType NewChallengeType(short challengeTypeID)
		{
			return new ChallengeType((short)challengeTypeID);
		}
		public IMatchState NewMatchState(short matchStateID )
		{
			return new MatchState(matchStateID);
		}

		public IMatchPlayerRequest NewMatchPlayerRequest(long id)
		{
         return new MatchPlayerRequest(id);
      }

      public IMatchPlayerRequest NewMatchPlayerRequest()
      {
         return new MatchPlayerRequest();
      }

      public IMatchTeamRequest NewMatchTeamRequest(long id)
		{
         return new MatchTeamRequest(id);
      }

		public IRequestStates NewRequestState(RequestState id)
		{
			return base.Resolve<IRequestStates, RequestState>(id);
		}
	}
}