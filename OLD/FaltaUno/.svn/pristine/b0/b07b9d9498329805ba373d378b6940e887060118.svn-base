using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IMatchPlayerRequest : IEntity
	{
		key<long> MatchPlayerRequestID { get; }

		IMatch Match { get; set; }
		IPlayer PlayerSender { get; set; }
		IPlayer PlayerReceiver { get; set; }
		IComment Comment { get; set; }

		IRequestStates MatchPlayerRequestState { get; set; }

		DateTime RecieveDate { get; set; }
		DateTime LastStateChangeDate { get; set; }

	}
}