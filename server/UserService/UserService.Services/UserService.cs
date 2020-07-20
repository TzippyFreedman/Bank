using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UserService.Api.Exceptions;
using UserService.Services.Exceptions;
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
        public async Task<bool> RegisterAsync(UserModel newUser, string password, string verificationCode)
        {
            EmailVerificationModel verification = await _userRepository.GetVerificationAsync(newUser.Email);

            if (verification.ExpirationTime > DateTime.Now)
            {
                throw new VerificationCodeExpiredException(verification.ExpirationTime);
            }
            if (verification.Code != verificationCode)
            {
                throw new InncorrectVerificationCodeException(verification.Code);
            }

            bool isUserExist = await _userRepository.CheckUserExistsAsync(newUser.Email);
            if (isUserExist)
            {
                throw new UserWithRequestedEmailExistsException(newUser.Email);
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

        //change return guid
        public async Task<Guid> LoginAsync(string email, string password)
        {
            UserModel user = await _userRepository.GetUserAsync(email);
            if (!Hash.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                Log.Information($"attemt to login for user with email:{email} failed!");
                return Guid.Empty;
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
            bool isUserExist = await _userRepository.CheckUserExistsAsync(emailVerification.Email);
            if (isUserExist)
            {
                throw new UserWithRequestedEmailExistsException(emailVerification.Email);
            }
            string vertificationCode = EmailVerification.GenerateVerificationCode();
            emailVerification.Code = vertificationCode;
            await _userRepository.AddVerificationAsync(emailVerification);
            EmailVerification.SendVertificationEmail(emailVerification.Email, vertificationCode);
        }


    }
}

