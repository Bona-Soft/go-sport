using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.SubEntities
{
	public interface IMatchType : IEntity
	{
      key<short> MatchTypeID { get; }

      ISport Sport { get; set; }
      IField DefaultField { get; set; }

      string Value { get; set; }
      string Description { get; set; }
      bool Default { get; set; }
      bool UniqueField { get; set; }
   }
}
