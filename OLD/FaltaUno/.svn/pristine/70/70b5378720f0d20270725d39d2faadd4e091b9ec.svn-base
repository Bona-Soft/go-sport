using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Entities.SubEntities
{
	public class MatchType : BaseEntity, IMatchType
	{
		public key<short> MatchTypeID { get; private set; }

      public ISport Sport { get; set; }
      public IField DefaultField { get; set; }

      public string Value { get; set; }
      public string Description { get; set; }
      public bool Default { get; set; }
      public bool UniqueField { get; set; }
		public short PlayersQuantity { get; set; }

		public MatchType(short matchTypeID)
		{
			MatchTypeID = matchTypeID;
		}

      public MatchType()
      {

      }
	}
}
