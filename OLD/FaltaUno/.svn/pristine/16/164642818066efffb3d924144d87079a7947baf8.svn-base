using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Entities
{
	public class Location : Entity, ILocation
	{
		public key<long> LocationID { get; }

		public ISport Sport { get; set; }
		public IUser User { get; set; }
		public int GroupMemberID { get; set; }

		public string Display { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }

      public string Value { get; set; }
      public string Country { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Address { get; set; }
		public string AddressNumber { get; set; }
		public string AddressFloor { get; set; }
		public string AddressApartament { get; set; }
		public string PostalCode { get; set; }

		public Location(long locationID)
		{
			LocationID = locationID;
		}

		public Location()
		{
         LocationID = 0;

      }
	}
}
