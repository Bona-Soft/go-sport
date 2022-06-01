using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Domain.Messenger
{
   public class BaseMessageState : IBaseMessageState
   {
      private readonly int _State;
      
      public int State { get { return _State; } }

      public string Value { get; set; }
      public string Description { get; set; }

      public bool Closed { get; set; }


      public static implicit operator BaseMessageState(int s)
      {
         return new BaseMessageState(s);
      }

      public static implicit operator int(BaseMessageState p)
      {
         return p.State;
      }


      public BaseMessageState(int id)
      {
         this._State = id;
         
      }

   }

}
