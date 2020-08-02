using Serilog;
using System;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Services.Exceptions;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerifier _emailVerifier;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IEmailVerifier emailVerifier, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _emailVerifier = emailVerifier;
            _passwordHasher = passwordHasher;
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
                string passwordSalt = _passwordHasher.CreateSalt();
                string passwordHash = _passwordHasher.CreatePasswordHash(password, passwordSalt);
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;
                await _userRepository.AddUserAsync(newUser);
                Log.Information("User with email {@email}  created successfully", newUser.Email);
            }
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
            UserModel user = await _userRepository.GetUserAsync(email);
            bool isPasswordCorrect = _passwordHasher.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                throw new IncorrectPasswordException(email);
            }
            AccountModel account = await _userRepository.GetAccountByUserIdAsync(user.Id);
            return account.Id;
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
            string vertificationCode = _emailVerifier.GenerateVerificationCode();
            emailVerification.Code = vertificationCode;
            await _userRepository.AddVerificationAsync(emailVerification);
            _emailVerifier.SendVerificationEmail(emailVerification.Email, vertificationCode);
        }
    }
}

