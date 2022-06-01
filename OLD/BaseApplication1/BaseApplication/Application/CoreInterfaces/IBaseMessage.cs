using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
   public interface IBaseMessage : IPerWebRequest
   {
      key<long> MessageID { get; }
      string Text { get; set; }
      IBaseUser UserSender { get; set; }

      IBaseMessageState State { get; set; }
      DateTime DateTimeCreated { get; set; }

   }
}
