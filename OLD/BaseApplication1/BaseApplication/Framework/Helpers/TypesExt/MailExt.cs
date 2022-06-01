using System.Net;
using System.Net.Mail;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
   public static class MailExt
   {
      /// <summary>
      ///   Add a new mail addres in To:
      /// </summary>
      public static void AddTo(this MailMessage mail, string emailTo)
      {
         mail.To.Add(new MailAddress(emailTo));
      }

      /// <summary>
      ///   Add a new mail addres in From:
      /// </summary>
      public static void AddFrom(this MailMessage mail, string emailFrom)
      {
         mail.From = new MailAddress(emailFrom);
      }

      /// <summary>
      ///   Set the mail and password credential to the Smpt Client.
      /// </summary>
      public static void SetCredentials(this SmtpClient smtpClient, string mailAddress, string password)
      {
         smtpClient.Credentials = new NetworkCredential(mailAddress, password);
      }
   }
}