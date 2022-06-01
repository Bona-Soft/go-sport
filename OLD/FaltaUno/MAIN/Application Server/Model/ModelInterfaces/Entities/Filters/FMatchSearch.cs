using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.Entities.Filters
{
   public interface FMatchSearch 
   {
      short SportID { get; set; }
      string Name { get; set; }
      long PlayerID { get; set; }
      short MinAge { get; set; }
      short MaxAge { get; set; }
      long LocationID { get; set; }
      int HeadquarterID { get; set; }
      float Lat { get; set; }
      float Lng { get; set; }
      short MatchTypeID { get; set; }
      short MatchFieldID { get; set; }
      DateTime StartDateTime { get; set; }
      DateTime EndDateTime { get; set; }
		bool Public { get; set; }
      bool IsHotMatch { get; set; }
   }

}
