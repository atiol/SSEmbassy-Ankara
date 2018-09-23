using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ssembassy_ankara.Services
{
    public interface IEmailNotifier
    {
        bool SendEmailNotification(string toMail, string subject, string body);
    }

    public class NotifyByEmail : IEmailNotifier
    {
        private readonly string _senderMail = System.Configuration.ConfigurationManager.AppSettings["senderMail"];
        private readonly string _password = System.Configuration.ConfigurationManager.AppSettings["mailPass"];

        public bool SendEmailNotification(string toMail, string subject, string body)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Timeout = 100000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_senderMail, _password)
                };

                // create mail message and send
                var mailMessage = new MailMessage(_senderMail, toMail, subject, body)
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                client.Send(mailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}