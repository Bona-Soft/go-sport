using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Entities.SubEntities
{
   public class SportSelected : ISportSelected
   {
      public short SportID { get; set; }

      public SportSelected()
      {
      }

      public SportSelected(short sportID)
      {
         SportID = sportID;
      }
   }
}
