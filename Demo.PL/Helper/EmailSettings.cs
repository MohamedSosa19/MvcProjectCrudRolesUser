using Demo.DAL.Models;
using System.Net.Mail;
using System.Net;

namespace Demo.PL.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {

            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("apikey", "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");
            client.Send("mohamedsosa626@gmail.com", email.To, email.Title, email.Body);

        }
    }
}
