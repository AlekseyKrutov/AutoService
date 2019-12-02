using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace AutoServiceLibrary
{
    class MailSending
    {
        public void SendMessage()
        {
            var fromAddress = new MailAddress("alekseykrutovvv@gmail.com", "From Name");
            var toAddress = new MailAddress("alekseykrutovvv@gmail.com", "To Name");
            const string fromPassword = "zegpwqbcbzsxooru";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
