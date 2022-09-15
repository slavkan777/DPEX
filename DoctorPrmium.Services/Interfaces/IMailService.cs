using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPremium.Services.Interfaces
{
	public interface IMailService
	{
		Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true);
		//void SendEmailAsync(string to, string subject, string body, string from, bool isBodyHtml = true);
	}
}
