using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace MYB.FaltaUno.WebServices.Authentication
{
	public partial class LoginGoogle : AppWebServices
	{
		//your client id  
		string clientid = "480438999449-ghc90fi67v2ia2oaupkst3e3k1h0h0sg.apps.googleusercontent.com";
		//your client secret  
		string clientsecret = "HCwRIvpmtmFk312QSkCKxvpQ";
		//your redirection url  
		string redirection_url = "http://www.gosport.com.ar";

		protected override void ProcessPOST()
		{
			
			string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=" + clientid + "";
			Response.Redirect(url);

			//string str = Data["param"].ToDefString();
			//JObject obj = (JObject)Data["JsonObj"];

			//ErrorManager.MandatoryError(str, TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.MandatoryError<JObject>(Data, "JsonObj", TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.Verify();

			//UIService.CreateEditUpdate(str, obj);

		}

		protected override void ProcessGET()
		{
				

			if(!IsPostBack) {
				if (Request.QueryString["code"] != null)
				{
					GetToken(Request.QueryString["code"].ToString());
				}
			}

			//JObject obj;
			//long ID = getRequestParam<long>("ID", 0);

			//ErrorManager.MandatoryError(ID, TranslationManager.Get("module", "constanteName", "Your default message"));
			//ErrorManager.Verify();

			//obj = UIService.Get(ID);
			//sendSerializeObject(obj);
		}

		public void GetToken(string code)
		{
			string url = "https://accounts.google.com/o/oauth2/token";
			string poststring = "grant_type=authorization_code&code=" + code + "&client_id=" + clientid + "&client_secret=" + clientsecret + "&redirect_uri=" + redirection_url + "";
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.ContentType = "application/x-www-form-urlencoded";
			request.Method = "POST";
			UTF8Encoding utfenc = new UTF8Encoding();
			byte[] bytes = utfenc.GetBytes(poststring);
			Stream outputstream = null;
			try
			{
				request.ContentLength = bytes.Length;
				outputstream = request.GetRequestStream();
				outputstream.Write(bytes, 0, bytes.Length);
			}
			catch { }
			var response = (HttpWebResponse)request.GetResponse();
			var streamReader = new StreamReader(response.GetResponseStream());
			string responseFromServer = streamReader.ReadToEnd();
			JavaScriptSerializer js = new JavaScriptSerializer();
			Tokenclass obj = js.Deserialize<Tokenclass>(responseFromServer);
			GetuserProfile(obj.access_token);
		}

		public void GetuserProfile(string accesstoken)
		{
			string url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + accesstoken + "";
			WebRequest request = WebRequest.Create(url);
			request.Credentials = CredentialCache.DefaultCredentials;
			WebResponse response = request.GetResponse();
			Stream dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);
			string responseFromServer = reader.ReadToEnd();
			reader.Close();
			response.Close();
			JavaScriptSerializer js = new JavaScriptSerializer();
			Userclass userinfo = js.Deserialize<Userclass>(responseFromServer);
			//imgprofile.ImageUrl = userinfo.picture;
			//lblid.Text = userinfo.id;
			//lblgender.Text = userinfo.gender;
			//lbllocale.Text = userinfo.locale;
			//lblname.Text = userinfo.name;
			//hylprofile.NavigateUrl = userinfo.link;
		}

		public class Tokenclass
		{
			public string access_token
			{
				get;
				set;
			}
			public string token_type
			{
				get;
				set;
			}
			public int expires_in
			{
				get;
				set;
			}
			public string refresh_token
			{
				get;
				set;
			}
		}
		public class Userclass
		{
			public string id
			{
				get;
				set;
			}
			public string name
			{
				get;
				set;
			}
			public string given_name
			{
				get;
				set;
			}
			public string family_name
			{
				get;
				set;
			}
			public string link
			{
				get;
				set;
			}
			public string picture
			{
				get;
				set;
			}
			public string gender
			{
				get;
				set;
			}
			public string locale
			{
				get;
				set;
			}
		}
	}
}