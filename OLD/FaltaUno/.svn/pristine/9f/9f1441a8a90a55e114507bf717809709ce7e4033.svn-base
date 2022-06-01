using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Entities.SubEntities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MYB.FaltaUno.Model.Entities
{
	public class MatchPlayerRequest : Entity, IMatchPlayerRequest
	{
		public key<long> MatchPlayerRequestID { get; private set; }

		public IMatch Match { get; set; }
		public IPlayer PlayerSender { get; set; }
		public IPlayer PlayerReceiver { get; set; }
		public IComment Comment { get; set; }

		public IRequestStates MatchPlayerRequestState { get; set; } 

		public DateTime RecieveDate { get; set; }
		public DateTime LastStateChangeDate { get; set; }

		

		public MatchPlayerRequest(long id)
		{
			MatchPlayerRequestID = id;
		}

		public MatchPlayerRequest()
		{
			MatchPlayerRequestID = 0;
		}


	}

}