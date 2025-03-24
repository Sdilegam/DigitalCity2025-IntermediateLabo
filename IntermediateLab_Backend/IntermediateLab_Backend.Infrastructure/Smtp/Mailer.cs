using System.Net.Mail;
using IntermediateLab_Backend.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace IntermediateLab_Backend.Infrastructure.Smtp;

/*
 * docker run --rm -it -p 5000:80 -p 2525:25 rnwood/smtp4dev
 */
public class Mailer(SmtpClient smtpClient, IConfiguration configuration): IMailer
{
	public void Send(string[] destinationArray, string subject, string body, params Attachment[] attachmentsArray)
	{
		MailMessage mMessage = new()
							   {
								   From = new MailAddress(configuration["Smtp:Username"] ?? throw new Exception("Missing smtp config")),
								   Subject = subject,
								   Body    = body,
								   IsBodyHtml = true
							   };
		foreach (string destination in destinationArray)
		{
			mMessage.To.Add(new MailAddress(destination));
		}
		foreach (Attachment attachment in attachmentsArray)
			mMessage.Attachments.Add(attachment);
		smtpClient.Send(mMessage);
	}
}