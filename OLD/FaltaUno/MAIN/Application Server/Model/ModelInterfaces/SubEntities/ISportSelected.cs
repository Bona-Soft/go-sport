using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.SubEntities
{
   public interface ISportSelected :IPerWebRequest
   {
      short SportID { get; set; }
   }
}
