using System.Net;
using System.Net.Mail;

namespace WebChecker
{
    public static class MessageService
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static string emailFromAddress = "momo420test@gmail.com"; //Sender Email Address  
        static string password = "test420M";                      //Sender Password  
        static string emailToAddress = "leatherpuffy@gmail.com"; //Receiver Email Address  
        static string subject = "Something unexpected happend!";

        public static void SendEmail(string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.EnableSsl = true;
                    smtp.Host = smtpAddress;
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.Send(mail);
                }
            }
        }
    }
}
