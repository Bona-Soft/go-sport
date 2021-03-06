using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities.SubEntities
{
	public class MatchPlayerRequestState : Entity, IRequestStates
	{
		public key<RequestState> RequestStateID { get; private set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public string DefaultComment { get; set; }
		public string Type { get; set; }

		public MatchPlayerRequestState(RequestState id)
		{
			RequestStateID = id;
		}
	}
}