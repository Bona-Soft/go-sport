namespace MYB.BaseApplication.Framework.Facebook
{
	public static class Facebook
	{
		public static void Login()
		{
			/*FaceBookConnect.API_Key = "<Your API Key>";
			FaceBookConnect.API_Secret = "<Your API Secret>";
			if (!IsPostBack)
			{
				if (Request.QueryString["error"] == "access_denied")
				{
					ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
					return;
				}

				string code = Request.QueryString["code"];
				if (!string.IsNullOrEmpty(code))
				{
					string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
					FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
					faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
					pnlFaceBookUser.Visible = true;
					lblId.Text = faceBookUser.Id;
					lblUserName.Text = faceBookUser.UserName;
					lblName.Text = faceBookUser.Name;
					lblEmail.Text = faceBookUser.Email;
					ProfileImage.ImageUrl = faceBookUser.PictureUrl;
					btnLogin.Enabled = false;
				}
			}*/
		}
	}
}