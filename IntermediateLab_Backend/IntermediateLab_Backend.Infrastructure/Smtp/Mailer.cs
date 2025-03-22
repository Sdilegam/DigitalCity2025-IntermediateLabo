using System.Net.Mail;
using IntermediateLab_Backend.Application.Interfaces;

namespace IntermediateLab_Backend.Infrastructure.Smtp;

public class Mailer: IMailer
{
	public void Send(string destination, string subject, string body, params Attachment[] attachments)
	{

		return ;
	}
}