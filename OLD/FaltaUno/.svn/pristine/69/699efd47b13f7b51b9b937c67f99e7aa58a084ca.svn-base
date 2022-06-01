using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.MainApplication.Services;
using MYB.FaltaUno.Application.UIService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;

namespace WebTest
{
   public partial class Debug : AppWebServices
   {
      protected override void ProcessPOST()
      {
      }

      protected override void ProcessGET()
      {
         string methodName = getRequestParam("method", "");

         Type thisType = this.GetType();
         MethodInfo theMethod = thisType.GetMethod(methodName);
         theMethod.Invoke(this, null);
      }

      public void ActualizarDB()
      {
         List<object> entities = new List<object>
         {
            BaseApp.Resolve<ILocationFactory>().New(),
            BaseApp.Resolve<ISportFactory>().New(),
            BaseApp.Resolve<IFieldFactory>().New(),
            BaseApp.Resolve<IHeadquarterFactory>().New(),
            BaseApp.Resolve<ITeamFactory>().New(),
            BaseApp.Resolve<IPlayerFactory>().New(),
            BaseApp.Resolve<IMatchFactory>().New()
         };

         string result = "";

         foreach (object entity in entities)
         {
            result += "****************************** " + entity.GetType().Name + "  ******************************" + Environment.NewLine + Environment.NewLine;
            result += Generics.GetSQLServerStrings.FullTableFromEntity(entity) + Environment.NewLine;
            result += "******************************************************************************************" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
         }

         Response.Write(result);
         Response.Flush();
      }

      public void RegisterUserStress()
      {
         //HttpContext ctx = HttpContext.Current;
         //new Thread(() =>
         //{
         // HttpContext.Current = ctx;
         //Thread.CurrentThread.IsBackground = true;

         string responseMessage = "";
         List<ISport> sports = SportService.GetSports();

         foreach (ISport sport in sports)
         {
            for (int i = 0; i < 10; i++)
            {
               try
               {
                  string unique = GuidGenerator.GetOne().ToString().Replace("-", "") + sport.SportID.Value.ToString() + i.ToString();
                  responseMessage += "UserID: " + UserUIService.RegisterUser(unique + "@" + unique + ".com", "1234Test", unique, unique, sport.SportID).ToString() + Environment.NewLine;
               }
               catch (Exception ex)
               {
                  responseMessage += "Error: " + ex.Message + Environment.NewLine;
               }
            }
         }
         //}).Start();
         Response.Write(responseMessage);
         Response.Flush();
      }

      public void VerifyAllUsers()
      {
         string responseMessage = "";

         DataTable dt = BaseApp.DB.Execute<DataTable>("SELECT * FROM LoginNames WHERE VerificationCode IS NOT NULL");

         foreach (DataRow dr in dt.Rows)
         {
            try
            {
               responseMessage += "Verify result for user " + dr["UserID"].ToString() + ": " + UserUIService.VerifyUser(dr["LoginName"].ToString(), dr["VerificationCode"].ToString()).ToString() + Environment.NewLine;
            }
            catch (Exception ex)
            {
               responseMessage += "Error message: " + ex.Message + Environment.NewLine;
            }
         }

         Response.Write(responseMessage);
         Response.Flush();
      }

      public void CreateMatchStress()
      {
         string responseMessage = "";

         List<ISport> sports = SportService.GetSports();

         foreach (ISport sport in sports)
         {
            for(int i = 0; i < 100; i++)
            {
               try
               {
                  JObject obj = JObject.Parse(@"{
                  Name: '" + GuidGenerator.GetOne().ToString().Replace("-", "") + @"', 
                  Sport: { 
                     SportID: " + sport.SportID.Value.ToString() + @", 
                     Name: '', 
                     Value: '', 
                     Fields: null
                  },
                  playerQty: {
                     MaxPlayers: 9,
                     MinPlayers: 4
                  },
                  MatchDateTime: '2018-08-22T23:00:00.000Z',
                  LocationGroup: {
                     Location: {
                        LocationID: 0,
                        Display: '1041 Middlefield Rd, Redwood City, CA 94063, EE. UU.',
                        Lat: 37.484386,
                        Lng: -122.226882,
                        Value: '1041 Middlefield Rd, Redwood City, CA 94063, EE. UU.'
                     }
                  }
               }");

                  responseMessage += "Match created: " + JsonConvert.SerializeObject(MatchUIService.Instance.CreateMatch(obj)) + Environment.NewLine;
               }
               catch (Exception ex)
               {
                  responseMessage += "Error message: " + ex.Message + Environment.NewLine;
               }
            }
         }

         Response.Write(responseMessage);
         Response.Flush();
      }

      public void POP_All()
      {
         POP_User();
      }

      public void POP_User()
      {
         UserUIService.RegisterUser("Bona3000@gmail.com", "871612Bona", "Martin", "Bonafede", 1);
         RegisterUserStress();
         BaseApp.DB.Execute<long>("UPDATE LoginNames SET Locked = 0, VerificationCode = NULL");
      }

      private string GET(string url)
      {
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
         try
         {
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
               StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
               return reader.ReadToEnd();
            }
         }
         catch (WebException ex)
         {
            WebResponse errorResponse = ex.Response;
            using (Stream responseStream = errorResponse.GetResponseStream())
            {
               StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
               String errorText = reader.ReadToEnd();
               // log errorText
            }
            throw;
         }
      }

      private void POST(string url, string jsonContent)
      {
         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
         request.Method = "POST";

         System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
         Byte[] byteArray = encoding.GetBytes(jsonContent);

         request.ContentLength = byteArray.Length;
         request.ContentType = @"application/json";

         using (Stream dataStream = request.GetRequestStream())
         {
            dataStream.Write(byteArray, 0, byteArray.Length);
         }
         long length = 0;
         try
         {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
               length = response.ContentLength;
            }
         }
         catch (WebException ex)
         {
            // Log exception and throw as for GET example above
         }
      }
   }
}