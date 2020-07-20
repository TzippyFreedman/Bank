using Serilog;
using System;
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

    }
}

