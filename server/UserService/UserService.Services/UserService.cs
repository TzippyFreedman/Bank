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
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailVerifier _emailVerifier;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IAccountRepository accountRepository, IEmailVerifier emailVerifier, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
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

            bool isUserExist = await _userRepository.IsExistsAsync(newUser.Email);
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
                await _userRepository.AddAsync(newUser);
                Log.Information("User with email {@email}  created successfully", newUser.Email);
            }
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
            UserModel user = await _userRepository.GetAsync(email);
            bool isPasswordCorrect = _passwordHasher.VerifyPassword(password, user.PasswordSalt, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                throw new IncorrectPasswordException(email);
            }
            AccountModel account = await _accountRepository.GetByUserIdAsync(user.Id);
            return account.Id;
        }


        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            UserModel user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task VerifyEmailAsync(EmailVerificationModel emailVerification)
        {
            bool isUserExist = await _userRepository.IsExistsAsync(emailVerification.Email);
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

