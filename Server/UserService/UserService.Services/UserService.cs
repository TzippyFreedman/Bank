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
                return false;
            }
            else
            {
                UserModel user = await _userRepository.AddUserAsync(newUser);

                AccountModel account = new AccountModel { UserId = user.Id };

                await  _userRepository.AddAccountAsync(account);

                return true;
            }
        }
    }
}

