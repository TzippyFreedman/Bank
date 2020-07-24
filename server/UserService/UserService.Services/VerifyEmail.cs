using Serilog.Settings.Configuration;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace UserService.Services
{
    public class VerifyEmail : IVerifyEmail
    {
        //move configuration to appsettings.json
        public void SendVerificationEmail(string emailAddress, string verificationCode)
        {
           

             var appSettings = ConfigurationManager.AppSettings;
             string senderEmailAddress = appSettings.Get("EmailAddress");
             string senderEmailPassword = appSettings.Get("EmailPassword");

             string SMTPHost = appSettings.Get("SMTPHost");

            using (MailMessage mail = new MailMessage())
            {
                //move hard code into config file!
                mail.From = new MailAddress(senderEmailAddress);
                mail.To.Add(emailAddress);
                mail.Subject = "Verification Code";
                mail.Body = $"Your Verification Code is: {verificationCode}";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(senderEmailAddress, 587))
                {
                    smtp.Host = SMTPHost;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(senderEmailAddress, senderEmailPassword);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                }
            }
        }

        public string GenerateVerificationCode()
        {
            //check if digits or digits+letters
            //Random rand = new Random();
            //rand.Next(1000, 9999);
            //  return rand.ToString();
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 4); ;
        }

    }
}
