using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.FaltaUno.Application.MainApplication.Services;
using MYB.FaltaUno.Model.Interfaces.Entities;
using Newtonsoft.Json.Linq;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.Application.UIService
{
   public class HeadquarterUIService : UIServices
   {
      #region "constructor"

      private static HeadquarterUIService m_Instancia;

      public static HeadquarterUIService Instancia
      {
         get
         {
            if (m_Instancia == null)
            {
               m_Instancia = new HeadquarterUIService();
            }
            return m_Instancia;
         }
      }

      public HeadquarterUIService()
      {
      }

      #endregion "constructor"

      public JArray SearchHeadquartes(string searchText, float lat, float lng, UIView uiView = UIView.Full)
      {
         return HeadquarterService.SearchHeadquarters(searchText, lat, lng).ToJArray(uiView);
      }

      public IHeadquarter Fill(JObject joHeadquarter)
      {
         IHeadquarter hq;
         if (joHeadquarter != null && joHeadquarter["HeadquarterID"] != null && joHeadquarter["HeadquarterID"].Value<int>() != 0)
         {
            hq = HeadquarterFactory.New(joHeadquarter["HeadquarterID"].Value<int>());
         }
         else
         {
            return null;
         }
         hq = joHeadquarter.ToClass(hq);

         return hq;
      }
   }
}