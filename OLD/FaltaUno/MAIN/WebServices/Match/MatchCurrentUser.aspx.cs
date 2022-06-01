using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MYB.FaltaUno.WebServices.Match
{
   public partial class MatchCurrentUser : AppWebServices
   {
      protected override void ProcessGET(Filter filter)
      {
         JArray arr = MatchUIService.GetCurrentUserMatches(filter);

         sendSerializeObject(arr);

      }


   }
}