using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Threading.Tasks;
using UserService.Services.Exceptions;
using UserService.Services.Interfaces;
using UserService.Services.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerifyEmail _verifyEmail;
        private readonly IHashPassword _hashPassword;

        public UserService(IUserRepository userRepository, IVerifyEmail verifyEmail, IHashPassword hashPassword)
        {
            _userRepository = userRepository;
            _verifyEmail = verifyEmail;
            _hashPassword = hashPassword;
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
                string passwordSalt = _hashPassword.CreateSalt();
                string passwordHash = _hashPassword.CreatePasswordHash(password, passwordSalt);
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;
                await _userRepository.AddUserAsync(newUser);
                Log.Information("User with email {@email}  created successfully", newUser.Email);
               
            }
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
            UserModel user = await _userRepository.GetUserAsync(email);
            bool isPasswordCorrect = _hashPassword.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);
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
            string vertificationCode = _verifyEmail.GenerateVerificationCode();
            emailVerification.Code = vertificationCode;
            await _userRepository.AddVerificationAsync(emailVerification);
            _verifyEmail.SendVerificationEmail(emailVerification.Email, vertificationCode);
        }
    }
}

