using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.SubEntities
{
   public interface IRequestStates : IPerConstructor
   {
      key<RequestState> RequestStateID { get; }
      string Value { get; set; }
      string Description { get; set; }
      string DefaultComment { get; set; }
      string Type { get; set; }
   }

   public static class RequestStatesTypes 
   {
      public const string INITIAL = "I";
      public const string POSITIVE = "P";
      public const string NEGATIVE = "N";
      public const string TENTATIVE = "T";
      public const string CLOSED = "C";
   }

   public enum RequestStateType
   {
      Initial = 'I',
      Positive = 'P',
      Negative = 'N',
      Tentative = 'T',
      Closed = 'C',
   }

   public enum RequestState
   {
      Pending = 1,
      Tentative_not,
      Tentative,
      Tentative_yes,
      Proposal,
      Confirmed,
      Reconfirmation_required,
      Rejected,
      Cancelled,
      Completed,
      Approval_required,
      Confirmed_substitute
   }
}
