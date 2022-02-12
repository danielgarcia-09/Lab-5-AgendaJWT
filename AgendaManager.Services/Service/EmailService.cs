using AgendaManager.Core.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgendaManager.Services.Service
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel emailModel);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task<bool> SendEmail(EmailModel emailModel)
        {
            var networkCredential = new NetworkCredential(_emailConfig.Email, _emailConfig.Password);
            var client = new SmtpClient(_emailConfig.Host)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = networkCredential,
                Port = 587,
                EnableSsl = true
            };

            var mailAddress = new MailAddress(_emailConfig.Email, _emailConfig.DisplayName);

            var mailMessage = new MailMessage()
            {
                Subject = "Invitation",
                SubjectEncoding = Encoding.UTF8,
                From = mailAddress,
                Body = emailModel.Message,
                BodyEncoding = Encoding.UTF8
            };

            foreach (var destinatary in emailModel.Destinataries)
            {
                mailMessage.To.Add(new MailAddress(destinatary));
            }

            client.Send(mailMessage);

            return true;
        }
    }

}
