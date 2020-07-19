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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            UserModel user = await _userRepository.GetUserAsync(email, password);

            if (user == null)
            {
                Log.Information($"attemt to login for user with email:{email} failed!");
                return Guid.Empty;
            }
            else
            {
                AccountModel userAccount = await _userRepository.GetUserAccountByUserIdAsync(user.Id);

                return userAccount.Id;
            }

        }
    }
}

