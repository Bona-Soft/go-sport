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
   public partial class MatchSearch : AppWebServices
   {
      protected override void ProcessGET(Filter data)
      {
         
         //DateTime matchStartTime;
         //if (!DateTime.TryParseExact(
         //      getRequestParam<string>("StarTime","00:00 AM")
         //         ,"HH:mm tt"
         //         ,CultureInfo.InvariantCulture
         //         ,DateTimeStyles.None
         //         ,out matchStartTime))
         //{
         //   matchStartTime = new DateTime(1,1,1,0,0,0);
         //}
         //DateTime matchEndTime;
         //if (!DateTime.TryParseExact(
         //      getRequestParam<string>("EndTime", "11:59 PM")
         //         , "HH:mm tt"
         //         , CultureInfo.InvariantCulture
         //         , DateTimeStyles.None
         //         , out matchEndTime))
         //{
         //   matchEndTime = new DateTime(1,1,1,23,59,59,999);
         //}


         JArray arr = MatchUIService.SearchMatches(data);

         sendSerializeObject(arr);

      }


   }
}