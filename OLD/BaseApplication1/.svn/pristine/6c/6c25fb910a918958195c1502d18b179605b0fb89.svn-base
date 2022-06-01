using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;

namespace MYB.BaseApplication.Domain.Mail
{
	public class MailManager : IMailManager
	{
		public MailMessage Mail { get; set; }
		public SmtpClient SMTP { get; set; }
		public Exception LastException { get; set; }

		private static Dictionary<string, Configuration> _Mails = new Dictionary<string, Configuration>();
		
		private class Configuration
		{
			public string Name { get; set; }
			public MailMessage Mail { get; set; }
			public SmtpClient SMTP { get; set; }

			public Configuration()
			{
				Mail = new MailMessage();
				SMTP = new SmtpClient();
			}
		}

		public MailManager(string configName)
		{
			Init();
			Load(configName);
		}

		public MailManager()
		{
			Init();
			Load("default");
		}

		public void Init()
		{
			if (_Mails == null || _Mails.Count == 0)
			{
				try
				{
					DataSet ds = BaseApp.WSP.ConfigurationGroup.GetMailServerConfiguration().Execute();
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						try
						{
							Configuration mailConfiguration = new Configuration();

							mailConfiguration.Name = row["Name"].ToString();
							mailConfiguration.Mail.Subject = row["Subject"].ToString(); ;
							mailConfiguration.Mail.Body = row["Body"].ToString(); ;
							mailConfiguration.Mail.IsBodyHtml = row["IsBodyHtml"].ToDefType(true);
							mailConfiguration.Mail.Priority = (MailPriority)row["Priority"].ToDefType(0);
							mailConfiguration.Mail.From = new MailAddress(row["From"].ToDefString());

							mailConfiguration.SMTP.Host = row["Host"].ToString(); ;
							mailConfiguration.SMTP.Port = row["Port"].ToDefType(587); ;
							mailConfiguration.SMTP.EnableSsl = row["EnableSsl"].ToDefType(true);
							mailConfiguration.SMTP.UseDefaultCredentials = row["UseDefaultCredentials"].ToDefType(false);
							mailConfiguration.SMTP.SetCredentials(row["MailAddress"].ToDefString(), row["Password"].ToDefString());

							_Mails.Add(mailConfiguration.Name, mailConfiguration);
						}
						catch { }
					}
				}
				catch { }
				if (_Mails.Count == 0)
				{
					Configuration mailConfiguration = new Configuration();

					mailConfiguration.Name = "default";

					mailConfiguration.Mail.Subject = "";
					mailConfiguration.Mail.Body = "";
					mailConfiguration.Mail.IsBodyHtml = true;
					mailConfiguration.Mail.Priority = MailPriority.Normal;
					mailConfiguration.Mail.From = new MailAddress("no.reply.mail.c3@gmail.com");

					mailConfiguration.SMTP.Host = "smtp.gmail.com";
					mailConfiguration.SMTP.Port = 587;
					mailConfiguration.SMTP.EnableSsl = true;
					mailConfiguration.SMTP.UseDefaultCredentials = false;
					mailConfiguration.SMTP.SetCredentials("no.reply.mail.c3@gmail.com", "Base.App");

					_Mails.Add("default", mailConfiguration);
				}
			}
		}

		public bool Load(string configurationName)
		{
			Configuration config = _Mails[configurationName];
			if (config != null)
			{
				try
				{
					Mail = config.Mail;
					SMTP = config.SMTP;
				}
				catch
				{
					return false;
				}
				return true;
			}
			return false;
		}

		public int Send(string configName)
		{
			try
			{
				if (!Load(configName))
				{
					return -2;
				}
				SMTP.Send(Mail);
				Mail.Dispose();
				LastException = null;
				return 0;
			}
			catch (Exception ex)
			{
				LastException = ex;
				return -1;
			}
		}

		public int Send()
		{
			try
			{
				SMTP.Send(Mail);
				Mail.Dispose();
				LastException = null;
				return 0;
			}
			catch (Exception ex)
			{
				LastException = ex;
				return -1;
			}
		}
	}
}