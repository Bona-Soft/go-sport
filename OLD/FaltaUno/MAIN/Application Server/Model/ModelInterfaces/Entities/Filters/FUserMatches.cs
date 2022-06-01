using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.Entities.Filters
{
   public interface FUserMatches 
   {
      long UserID{ get; set; }
      short SportID { get; set; }
      string MatchRequestType { get; set; }
      short MatchStateID { get; set; }
      bool Closed { get; set; }
      short PageNumber { get; set; }
      int PageSize { get; set; }
      bool AscOrder { get; set; }
      
   }

}
