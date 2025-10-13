using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com",587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("xmuha01@gmail.com", "snirugwsnwcdstyl");
            Client.Send("xmuha01@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
