using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(UserModel newUser)
        {
            bool isEmailExist = await _userRepository.CheckEmailExistsAsync(newUser.Email);

            if (isEmailExist)
            {
                Log.Information("User with email {@email} requested to create but already exists", newUser.Email);
                return false;
            }
            else
            {
                UserModel user = await _userRepository.AddUserAsync(newUser);

                AccountModel account = new AccountModel { UserId = user.Id };

                await  _userRepository.AddAccountAsync(account);

                Log.Information("User with email {@email}  created successfully", newUser.Email);

                return true;
            }
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
            UserModel user = await _userRepository.GetUser(email, password);

            if (user == null)
            {
                Log.Information($"attemt to login for user with email:{email} failed!");
                return Guid.Empty;
            }
            else
            {
                AccountModel userAccount = await _userRepository.GetUserAccountByUserId(user.Id);

                return userAccount.Id;
            }

        }
    }
}

