using Serilog;
using System;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Helpers.Interfaces;
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
            //double check user does not exist in case function is called in some other way. 
            bool isUserExist = await _userRepository.IsExistsAsync(newUser.Email);
            if (isUserExist)
            {
                throw new UserWithRequestedEmailAlreadyExistsException(newUser.Email);
            }
            EmailVerificationModel verification = await _userRepository.GetVerificationCodeAsync(newUser.Email);
            if (verification.Code != verificationCode)
            {
                throw new IncorrectVerificationCodeException(verificationCode);
            }
            if (verification.ExpirationTime < DateTime.Now)
            {
                throw new VerificationCodeExpiredException(verification.ExpirationTime);
            }
            string passwordSalt = _passwordHasher.CreateSalt();
            string passwordHash = _passwordHasher.CreatePasswordHash(password, passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            await _userRepository.AddAsync(newUser);
            Log.Information("User with email {@email}  created successfully", newUser.Email);
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

        public async Task AddVerificationAsync(EmailVerificationModel emailVerification)
        {
            bool isUserExist = await _userRepository.IsExistsAsync(emailVerification.Email);
            if (isUserExist)
            {
                throw new UserWithRequestedEmailAlreadyExistsException(emailVerification.Email);
            }
            string verificationCode = _emailVerifier.GenerateVerificationCode();
            emailVerification.Code = verificationCode;
            await _userRepository.AddVerificationCodeAsync(emailVerification);
            await _emailVerifier.SendVerificationEmailAsync(emailVerification.Email, verificationCode);
        }
    }
}

