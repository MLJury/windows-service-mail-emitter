using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Infrastructure.Mail
{
    internal abstract class Service
    {
        public Service()
        {
        }
        SmtpClient _client;
        MailMessage _message;

        protected virtual Task<AppCore.Result> SendCoreAsync(Core.Model.Mail mail, Core.Model.Account account)
        {
            _client = new SmtpClient()
            {
                Host = account.Host,
                Port = account.Port,
                EnableSsl = account.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(account.Email, account.Password),
                Timeout = 20000
            };

            _message = new MailMessage()
            {
                Body = mail.Content,
                BodyEncoding = Encoding.UTF8,
                Subject = mail.Subject,
                SubjectEncoding = Encoding.UTF8,
                From = new MailAddress(account.Email),
                IsBodyHtml = true
            };

            mail.EmailReceivers.ForEach(f =>
            {
                if (!String.IsNullOrEmpty(f.Email))
                    _message.To.Add(f.Email);
                if (!String.IsNullOrEmpty(f.Cc))
                    _message.CC.Add(f.Cc);
                if (!String.IsNullOrEmpty(f.Bcc))
                    _message.Bcc.Add(f.Bcc);
            });

            _client.SendMailAsync(_message);

            return AppCore.Result.SuccessfulAsync();
        }
    }
}
