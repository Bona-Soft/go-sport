using System.Net;
using System.Net.Mail;

namespace MYB.BaseApplication.Domain.Mail
{
	public static class MailExt
	{
		public static void AddTo(this MailMessage mail, string emailTo)
		{
			mail.To.Add(new MailAddress(emailTo));
		}

		public static void AddFrom(this MailMessage mail, string emailFrom)
		{
			mail.From = new MailAddress(emailFrom);
		}

		public static void SetCredentials(this SmtpClient smtpClient, string mailAddress, string password)
		{
			smtpClient.Credentials = new NetworkCredential(mailAddress, password);
		}
	}
}