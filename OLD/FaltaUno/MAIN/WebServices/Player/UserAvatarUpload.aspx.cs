using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Web;

namespace MYB.FaltaUno.WebServices.Player
{
   public partial class UserAvatarUpload : AppWebServices
   {
      protected override void ProcessPOST()
      {
         JObject response = new JObject();
         string newAvatar = string.Empty;

         //VALIDACIONES
         if (Request.Files.Count > 1)
         {
            ErrorManager.AddError(TranslationManager.Get("PlayerAvatarUpload", "TooManyFiles", "Upload only one image"));
         }
         else if (Request.Files.Count == 0)
         {
            ErrorManager.AddError(TranslationManager.Get("PlayerAvatarUpload", "NoFileToUpload", "No file was selected to upload"));
         }

         ErrorManager.Verify();

         HttpPostedFile file = Request.Files[0];

         if (file.ContentType.ToString().IndexOf("image") < 0)
         {
            ErrorManager.AddError(TranslationManager.Get("PlayerAvatarUpload", "FileIsNotAnImage", "Unsupported format"));
         }

         if (file.ContentLength > BaseApp.GeneralParameters.Get("AvatarImageMaxLength", 4194304))
         {
            ErrorManager.AddError(TranslationManager.Get("PlayerAvatarUpload", "ImageTooLarge", "The image is too large"));
         }

         ErrorManager.Verify();

         try
         { 
            using (Image img = Image.FromStream(file.InputStream, true, true))
            {
               newAvatar = UserUIService.UpdateAvatar(img,file.FileName);
            }
         }
         catch
         {
            //BaseApp.ErrorManager.AddError(BaseApp.TranslationManager.Get("UploadAvatar", "UploadFileAvatarError", "File could not be saved"));
         }
        // BaseApp.ErrorManager.Verify();

         response.Add("Avatar", newAvatar);

         sendSerializeObject(response);
      }
   }
}