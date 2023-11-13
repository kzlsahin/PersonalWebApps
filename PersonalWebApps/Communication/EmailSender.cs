using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace tracker_webapp.Communication
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _hostMail;
        public EmailSender(string host, int port, MailAddress from, NetworkCredential credentials, bool enableSsl) 
        { 
            _smtpClient = new SmtpClient(host)
            {
                Port = port,
                Credentials = credentials,
                EnableSsl = enableSsl,
            };
            _hostMail = from;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage(_hostMail.Address, email);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = htmlMessage;
            return _smtpClient.SendMailAsync(message);
        }

        public void SendMail(string reciepient, string subject, string body)
        {
            _smtpClient.Send(_hostMail.Address, reciepient, subject, body);
        }
        public void SendMail(MailMessage message)
        {
            message.From = _hostMail;
            _smtpClient.Send(message);
        }
    }
}
