using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace AutoServiceLibrary
{
    public class Mail
    {
        private const string fromCredential = "yjbgnanmeeevrryf";
        private const string fromEmail = "alekseykrutovvv@gmail.com";

        private MailAddress toAddress;
        private MailAddress fromAddress;
        private SmtpClient smtp;

        public Client Client { get; set; }
        public SystemOwner SystemOwner { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string[] Attachments { get; set; }
        public Mail() { }
        public Mail(Client client, SystemOwner systemOwner, string subject, string body, string[] attachments)
        {
            Client = client;
            SystemOwner = systemOwner;
            Subject = Subject;
            Body = body;
            fromAddress = new MailAddress(SystemOwner.Email, systemOwner.Name);
            toAddress = new MailAddress(Client.Email, client.Name);
            Attachments = attachments;
            smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromCredential)
            };
        }

        public void SendMessage()
        {
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = this.Subject,
                Body = this.Body
            })
            {
                foreach (string a in Attachments)
                {
                    Attachment data = new Attachment(a, MediaTypeNames.Application.Octet);
                    message.Attachments.Add(data);
                }
                smtp.Send(message);
            }
        }
    }
}
