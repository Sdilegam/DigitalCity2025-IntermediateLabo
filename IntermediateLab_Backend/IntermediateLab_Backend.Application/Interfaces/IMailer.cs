using System.Net.Mail;

namespace IntermediateLab_Backend.Application.Interfaces;
/* sh command to run smtp4dev docker:
 * docker run --rm -it -p 5000:80 -p 2525:25 rnwood/smtp4dev
 */


public interface IMailer
{
	void Send(string destination, string subject, string body, params Attachment[] attachments);
}