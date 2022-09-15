using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;
using System.Web.Configuration;

namespace DoctorPremium.Services
{
	public class MailService : IMailService
	{
		public Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
		{
			MailMessage mail = new MailMessage();

			MailAddress from = new MailAddress(WebConfigurationManager.AppSettings["Email"]);

			mail.From = from;
			mail.To.Add(new MailAddress(to));
			mail.Bcc.Add(from);
			mail.Subject = subject;
			mail.Body = body;
			mail.IsBodyHtml = isBodyHtml;

			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["Email"], WebConfigurationManager.AppSettings["EmailPass"]);

			//try
			//{
				smtpClient.SendCompleted += (s, e) =>
				{
					smtpClient.Dispose();
					mail.Dispose();
				};

			return smtpClient.SendMailAsync(mail);
				//smtpClient.Send(mail);//await smtpClient.SendMailAsync(mail); //state
			//}
			//catch (Exception ex)
			//{
			//	//string s = ex.Message;
			//	return Task.FromResult(0);
			//	throw ex;
			//}
		}
	}
}
