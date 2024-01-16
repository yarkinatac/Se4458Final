using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using System;

namespace BloodBankWebAPI.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public bool SendMail(string emailAddress, string subject, string body, IConfiguration configuration)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(
                    _configuration["ServiceMail:Email"] ?? throw new ArgumentNullException("ServiceMail:Email is not configured")
                ));
                email.To.Add(MailboxAddress.Parse(emailAddress));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = "<p>" + body + "</p>"
                };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(
                    _configuration["ServiceMail:Email"] ?? throw new ArgumentNullException("ServiceMail:Email is not configured"),
                    _configuration["ServiceMail:Password"] ?? throw new ArgumentNullException("ServiceMail:Password is not configured")
                );
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your need
                return false;
            }
        }
    }
}