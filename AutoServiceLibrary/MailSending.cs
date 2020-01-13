using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace AutoServiceLibrary
{
    public class MailSending
    {
        public void SendMessage(string toMail, string Name)
        {
            var fromAddress = new MailAddress("alekseykrutovvv@gmail.com", "Aleksey Krutov");
            var toAddress = new MailAddress(toMail, "To Name");
            const string fromPassword = "yjbgnanmeeevrryf";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 465,
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
