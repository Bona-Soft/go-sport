using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IUserPrivacy : IDictionary<string, Dictionary<long, string, PrivacyValue>>, ICloneable
	{
      void Add(long userPrivacyID, string entity, string property, PrivacyValue value);
		bool Remove(string entity, string property);

	}

   public enum PrivacyValue
   {
      Nobody = 'N',
      Everybody = 'E',
      Partial = 'P'
   }
}
