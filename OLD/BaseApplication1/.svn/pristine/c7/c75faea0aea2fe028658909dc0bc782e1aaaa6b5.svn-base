using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System;
using System.Net.Mail;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IMailManager : IPerWebRequest
	{
		MailMessage Mail { get; set; }
		SmtpClient SMTP { get; set; }
		Exception LastException { get; set; }

		int Send();
		int Send(string configName);

		void Init();
		bool Load(string configurationName);
	}
}