using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderExample
{
    static class EmailSender
    {
        public static void Send(string to, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp-mail.outlook.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("example@outlook.com", "zortingen");
            smtpClient.Credentials = credential;

            MailAddress gonderen = new MailAddress("example@outlook.com", "Rabbit ile SignalR Example");
            MailAddress alici = new MailAddress(to);

            MailMessage mail = new MailMessage(gonderen, alici);
            mail.Subject = "Example";
            mail.Body = message;

             smtpClient.Send(mail);

        }
    }
}
