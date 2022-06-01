using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Entities.SubEntities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{
	public class MatchTeamRequest : Entity, IMatchTeamRequest
	{
		public key<long> MatchTeamRequestID { get; private set; }

		public IPlayer PlayerRequestReceiver { get; set; }
		public IPlayer PlayerRequestSender { get; set; }

		public IRequestStates RequestState { get; set; }

		public MatchTeamRequest(long matchTeamRequestID)
		{
			MatchTeamRequestID = matchTeamRequestID;
		}

		public MatchTeamRequest()
		{
			MatchTeamRequestID = 0;
		}
	}
}