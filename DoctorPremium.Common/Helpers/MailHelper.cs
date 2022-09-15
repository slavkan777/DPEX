using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace DoctorPremium.Common.Helpers
{
	public static class MailHelper
	{
		public static void SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
		{
			SendEmailAsync(to, subject, body, WebConfigurationManager.AppSettings["Email"], isBodyHtml);
		}

		public static void SendEmailAsync(string to, string subject, string body, string from, bool isBodyHtml = true)
		{
			MailMessage mail = new MailMessage();

			mail.From = new MailAddress(from);
			mail.To.Add(new MailAddress(to));
			mail.Subject = subject;
			mail.Body = body;
			mail.IsBodyHtml = isBodyHtml;

			SmtpClient smtpClient = new SmtpClient();
			Object state = mail;

			//event handler for asynchronous call
			smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
			try
			{
				smtpClient.SendMailAsync(mail); //
			}
			catch (Exception ex)
			{
				string s = ex.Message;
				throw ex;
			}
			finally
			{
				mail.Dispose();
			}
		}

		static void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			MailMessage mail = e.UserState as MailMessage;

			if (!e.Cancelled && e.Error != null)
			{
				string s = "success";
			}
		}
	}
}
