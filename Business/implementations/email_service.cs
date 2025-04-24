using Business.Contracts;
using Data.contracts;
using Data.Contracts;
using Domain;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{

        public class EmailClass
        {
            public string To { get; set; }  = "";

            public string Subject { get; set; } = "";

            public string Body { get; set; } = "";
        }


    public class EmailService : IEmailService
    {


        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool sendMail(
            int id,
            EmailClass emailClass

            )
            {

                var email  = new MimeMessage();

                email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailSettings:UserName").Value));
                email.To.Add(MailboxAddress.Parse(emailClass.To));
                email.Subject = emailClass.Subject;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailClass.Body
                };

                var smtp = new SmtpClient();

                smtp.Connect(_configuration.GetSection("EmailSettings:Host").Value, int.Parse(_configuration.GetSection("EmailSettings:Port").Value), MailKit.Security.SecureSocketOptions.StartTls);
                

                smtp.Authenticate(_configuration.GetSection("EmailSettings:UserName").Value, _configuration.GetSection("EmailSettings:PassWord").Value);

                smtp.Send(email);
                return true;
            }

        





    }

}