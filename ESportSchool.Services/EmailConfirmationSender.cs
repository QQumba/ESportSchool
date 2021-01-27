using System;
using System.Net;
using System.Net.Mail;
using ESportSchool.Services.Utils;

namespace ESportSchool.Services
{
    public class EmailConfirmationSender
    {
        private readonly EmailMessageBuilder _builder;
        
        public EmailConfirmationSender(EmailMessageBuilder builder)
        {
            _builder = builder;
        }
        
        // TODO template different message based on emailMessageBuilder instance
        public void SendMessage(string destinationEmail, string message) 
        {
            MailAddress from = new MailAddress("somemail@gmail.com", "Tom");
            MailAddress to = new MailAddress(destinationEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>" +
                     "<hr>" +
                     "<p>1st paragraph of test</p>";
            m.IsBodyHtml = true;
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("somemail@gmail.com", "mypassword"),
                EnableSsl = true
            };
            smtp.Send(m);
        }
    }
}