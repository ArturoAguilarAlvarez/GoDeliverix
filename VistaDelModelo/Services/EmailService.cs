using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo.Services
{
    public class EmailService
    {
        public EmailService() // Constructor
        {
            // First
        }

        public void SendEmail(string body, string to, string subject, IEnumerable<string> cc = null, string lang = "en")
        {
            /*
             * Test
             */
            to = "hectorb.peraza@gmail.com";

            MailMessage mail = new MailMessage(ApplicationVars.GoEmail, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ApplicationVars.EmailHost;
            smtp.EnableSsl = false;

            NetworkCredential credenciales = new NetworkCredential(ApplicationVars.GoEmail, ApplicationVars.EmailPassword);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = credenciales;
            smtp.Port = 587;
            smtp.Send(mail);
        }
    }
}
