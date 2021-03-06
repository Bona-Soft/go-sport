using MYB.BaseApplication.Framework.Helpers;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface ILocation : IEntity
	{
		key<long> LocationID { get; }

		ISport Sport { get; set; }
		IUser User { get; set; }
		int GroupMemberID { get; set; }

		string Display { get; set; }
		double Lat { get; set; }
		double Lng { get; set; }

		string Value { get; set; }
		string Country { get; set; }
		string City { get; set; }
		string State { get; set; }
		string Address { get; set; }
		string AddressNumber { get; set; }
		string AddressFloor { get; set; }
		string AddressApartament { get; set; }
		string PostalCode { get; set; }
	}
}