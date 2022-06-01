using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Entities.SubEntities
{
	public class MatchState : BaseEntity, IMatchState
	{

		public key<short> MatchStateID { get; private set; }

      public string Value { get; set; }
      public string Description { get; set; }
      public bool Closed { get; set; }

      public MatchState(short matchStateID)
		{
         MatchStateID = matchStateID;
		}
	}
}
