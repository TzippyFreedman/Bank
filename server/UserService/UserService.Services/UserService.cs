using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<bool> RegisterAsync(UserModel newUser, string password)
        {

            bool isEmailExist = await _userRepository.CheckEmailExistsAsync(newUser.Email);

            if (isEmailExist)
            {
                Log.Information("User with email {@email} requested to create but already exists", newUser.Email);
                return false;
            }
            else
            {
                string passwordSalt = Hash.CreateSalt();
                string passwordHash = Hash.CreatePasswordHash(password, passwordSalt);

                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;

                await _userRepository.AddUserAsync(newUser);
                Log.Information("User with email {@email}  created successfully", newUser.Email);
                return true;
            }
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
            UserModel user = await _userRepository.GetUserAsync(email);

            if (user == null)
            {
                Log.Information($"attemt to login for user with email:{email} failed!");
                return Guid.Empty;
            }

            if (!Hash.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                Log.Information($"attemt to login for user with email:{email} failed!");
                return Guid.Empty;
            }

            AccountModel userAccount = await _userRepository.GetAccountByUserIdAsync(user.Id);

            return userAccount.Id;

        }

        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            AccountModel account = await _userRepository.GetAccountByIdAsync(accountId);

            return account;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            UserModel user = await _userRepository.GetUserByIdAsync(id);


            return user;
        }

        public async Task VerifyEmailAsync(EmailVerificationModel emailVerification)
        {
           string vertificationCode = GenerateVerificationCode();
            emailVerification.Code = vertificationCode;
          await  _userRepository.AddVerificationAsync(emailVerification);
            SendEmail(emailVerification.Email, vertificationCode);

        }
        private void SendEmail(string emailAddress,string vertificationCode)
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
        private string GenerateVerificationCode()
        {
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 4); ;
        }
    }
}

