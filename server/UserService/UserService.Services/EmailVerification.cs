using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace UserService.Services
{
  static public class EmailVerification
    {
        static public void SendVertificationEmail(string emailAddress, string vertificationCode)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("tzippyfreedman1@gmail.com"); //enter whatever email you are sending from here 
                mail.To.Add(emailAddress); //Text box that the user enters their email address 
                mail.Subject = "Email Vertification"; //enter whatever subject you would like 
                mail.Body = $"Your Activation Code is: {vertificationCode}";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("tzippyfreedman1@gmail.com", 587)) //enter the same email that the message is sending from along with port 587
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("tzippyfreedman1@gmail.com", "Tf0583265366"); //Enter email with password 
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(mail);
                }


            }
        }
        public static string GenerateVerificationCode()
        {
            //Random rand = new Random();
            //rand.Next(1000, 9999);
            //  return rand.ToString();
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 4); ;
        }

    }
}
