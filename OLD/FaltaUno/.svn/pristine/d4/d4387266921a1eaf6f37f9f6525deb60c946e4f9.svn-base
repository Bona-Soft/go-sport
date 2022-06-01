using MYB.BaseApplication.Framework.Helpers;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IPlayer : IEntity
	{
		key<long> PlayerID { get; }
		string Alias { get; set; }
		bool Active { get; set; }

		IUser User { get; set; }
		ISport Sport { get; set; }
		
		
		List<IField> Fields { get; set; }
		List<IChallengeType> ChallengeTypes { get; set; }
		List<IFieldPosition> PlayerPositions { get; set; }


	}
}