using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IChallengeType : IEntity
	{
      key<short> ChallengeTypeID { get;  }

      string Value { get; set; }
      string Description { get; set; }
      bool Default { get; set; }
   }
}
