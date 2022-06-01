using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IMatchTeamRequest : IEntity
	{
		key<long> MatchTeamRequestID { get; }

		IPlayer PlayerRequestReceiver { get; set; }
		IPlayer PlayerRequestSender { get; set; }

		IRequestStates RequestState { get; set; }
	}
}