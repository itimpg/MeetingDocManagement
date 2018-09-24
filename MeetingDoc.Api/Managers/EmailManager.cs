using System.Net;
using System.Net.Mail;
using MeetingDoc.Api.Managers.Interfaces;
using MeetingDoc.Api.ViewModels;
using Microsoft.Extensions.Configuration;

namespace MeetingDoc.Api.Managers
{
    public class EmailManager : IEmailManager
    {
        private IConfiguration _config;
        public EmailManager(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(EmailViewModel email)
        {
            var emailSettings = _config.GetSection("EmailSettings");
            var host = emailSettings.GetValue<string>("smtpHost");
            var port = emailSettings.GetValue<int>("smtpPort");
            var username = emailSettings.GetValue<string>("username");
            var password = emailSettings.GetValue<string>("password");
            var mailFrom = emailSettings.GetValue<string>("mailFrom");

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };
            var mailMessage = new MailMessage(mailFrom, email.EmailTo, email.Subject, email.Body);
            if (email.Attachment != null)
            {
                mailMessage.Attachments.Add(email.Attachment);
            }
            client.Send(mailMessage);
        }
    }
}