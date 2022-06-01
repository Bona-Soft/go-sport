using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Model.Entities.SubEntities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Entities
{

	public class UserPrivacy : Dictionary<string, Dictionary<long, string, PrivacyValue>>, IUserPrivacy
	{
		private long UserID { get; set; }

      public void Add(long userPrivacyID, string entity, string property, PrivacyValue value)
      {
         if (base.ContainsKey(entity))
         {
            if (base[entity].ContainsKey(userPrivacyID,property))
            {
               base[entity].Remove(userPrivacyID, property);
            }

            base[entity].Add(userPrivacyID, property, value);  
         }
         else
         {
            Dictionary<long, string, PrivacyValue> aux = new Dictionary<long, string, PrivacyValue>();
            aux.Add(userPrivacyID, property, value);
            base.Add(entity, aux);
         }
      }

		public IUserPrivacy Clone()
		{
			return (IUserPrivacy)this.MemberwiseClone();
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		public bool Remove(string entity, string property)
		{
			return base[entity].Remove(Tuple.Create<long,string>(0, property));
		}

		public UserPrivacy(long userID)
		{
         UserID = userID;

      }
	}
}