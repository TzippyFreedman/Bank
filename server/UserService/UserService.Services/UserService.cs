using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Threading.Tasks;
using UserService.Services.Exceptions;
using UserService.Services.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly SmtpSettings _smtpSettings;

        public UserService(IUserRepository userRepository,SmtpSettings smtpSettings)
        {
            _userRepository = userRepository;
            _smtpSettings = smtpSettings;
        }

        public async Task RegisterAsync(UserModel newUser, string password, string verificationCode)
        {
            EmailVerificationModel verification = await _userRepository.GetVerificationAsync(newUser.Email);
            if (verification.Code != verificationCode)
            {
                throw new IncorrectVerificationCodeException(verificationCode);
            }
            if (verification.ExpirationTime < DateTime.Now)
            {
                throw new VerificationCodeExpiredException(verification.ExpirationTime);
            }

            bool isUserExist = await _userRepository.IsUserExistsAsync(newUser.Email);
            if (isUserExist)
            {
                throw new UserWithRequestedEmailAlreadyExistsException(newUser.Email);
            }
            else
            {
                string passwordSalt = Hash.CreateSalt();
                string passwordHash = Hash.CreatePasswordHash(password, passwordSalt);
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;
                await _userRepository.AddUserAsync(newUser);
                Log.Information("User with email {@email}  created successfully", newUser.Email);
               
            }
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
            UserModel user = await _userRepository.GetUserAsync(email);
            bool isPasswordCorrect = Hash.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                throw new IncorrectPasswordException(email);
            }
            AccountModel account = await _userRepository.GetAccountByUserIdAsync(user.Id);
            return account.Id;
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
            bool isUserExist = await _userRepository.IsUserExistsAsync(emailVerification.Email);
            if (isUserExist)
            {
                throw new UserWithRequestedEmailAlreadyExistsException(emailVerification.Email);
            }
            string vertificationCode = EmailVerification.GenerateVerificationCode();
            emailVerification.Code = vertificationCode;
            await _userRepository.AddVerificationAsync(emailVerification);
            EmailVerification.SendVerificationEmail(emailVerification.Email, vertificationCode);
        }
    }
}

