using System.Net;
using System.Net.Mail;

namespace SWP391API.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SmtpClient getStmpClient()
        {
            var smtpSection = _configuration.GetSection("Smtp");

            var email = smtpSection["Email"];
            var password = smtpSection["Password"];

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            return smtpClient;
        }

        public void sendTo(string to, string title, string body)

        {
            var smtpClient = getStmpClient();
            
            smtpClient.Send("khangzxrr3@gmail.com", to, title, body);
        }

        public void sendToWithPdfAttachment(string to, string title, string body, byte[] pdfContent, string pdfName)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("khangzxrr3@gmail.com");
            mailMessage.To.Add(to);
            mailMessage.Subject = title;
            mailMessage.Body = body;

            MemoryStream stream = new MemoryStream(pdfContent);
            mailMessage.Attachments.Add(new Attachment(stream, pdfName, "application/pdf"));

            var smtpClient = getStmpClient();
            smtpClient.Send(mailMessage);

            stream.Close();
        }
    }
}
