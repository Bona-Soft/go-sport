using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;

namespace MYB.FaltaUno.Model.Entities.SubEntities
{
	public class ChallengeType : BaseEntity, IChallengeType
	{


		public key<short> ChallengeTypeID { get; private set; }

      public string Value { get; set; }
      public string Description { get; set; }
      public bool Default { get; set; }

      public ChallengeType(short challengeTypeID)
		{
         ChallengeTypeID = challengeTypeID;
		}
	}
}