using Facebook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Face
{
   public class Class1
   {
      private const string AppId = "Put your application id provided by facebook";
      public FacebookOAuthResult _FacebookOAuthResult { get; private set; }
      private const string _ExtendedPermissions = "user_about_me,publish_stream,offline_access";

      /*
      protected List<Facebook.User> GetFacebookUserData(string code)
      {
         // Exchange the code for an access token
         Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/account/user.aspx&code=" + code);
         HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

         System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
         string token = str.ReadToEnd().ToString().Replace("access_token=", "");

         // Split the access token and expiration from the single string
         string[] combined = token.Split('&');
         string accessToken = combined[0];

         // Exchange the code for an extended access token
         Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
         HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

         StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
         string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

         // Split the access token and expiration from the single string
         string[] eatWords = eatToken.Split('&');
         string extendedAccessToken = eatWords[0];

         // Request the Facebook user information
         Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,gender,locale,link&access_token=" + accessToken);
         HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

         // Read the returned JSON object response
         StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
         string jsonResponse = string.Empty;
         jsonResponse = userInfo.ReadToEnd();

         // Deserialize and convert the JSON object to the Facebook.User object type
         JavaScriptSerializer sr = new JavaScriptSerializer();
         string jsondata = jsonResponse;
         Facebook.User converted = sr.Deserialize<Facebook.User>(jsondata);

         // Write the user data to a List
         List<Facebook.User> currentUser = new List<Facebook.User>();
         currentUser.Add(converted);

         // Return the current Facebook user
         return currentUser;
      }*/
   }
}
