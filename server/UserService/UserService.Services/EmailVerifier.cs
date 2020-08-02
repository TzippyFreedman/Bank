using System.IO;
using System.Net;
using System.Net.Mail;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class EmailVerifier : IEmailVerifier
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailVerifier(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }
        public void SendVerificationEmail(string emailAddress, string verificationCode)
        {
            string senderEmailAddress = _smtpSettings.Address;
            string senderEmailPassword = _smtpSettings.Password;

            string SMTPHost = _smtpSettings.SMTPHost;

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
            //check if to return digits or digits+letters
            //Random rand = new Random();
            //rand.Next(1000, 9999);
            //  return rand.ToString();
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 4); ;
        }

    }
}
