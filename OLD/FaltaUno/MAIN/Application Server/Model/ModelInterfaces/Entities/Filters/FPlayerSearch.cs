using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.Entities.Filters
{
   public interface FPlayerSearch
   {
		long UserID { get; set; }
		short SportID { get; set; }
		string Alias { get; set; }
      string Name { get; set; }
      string LastName { get; set; }
      short MinAge { get; set; }
      short MaxAge { get; set; }
		string Email { get; set; }
		string MobileNumber { get; set; }
		long LocationID { get; set; }
		int HeadquarterID { get; set; }
		float Lat { get; set; }
		float Lng { get; set; }
	}
}
