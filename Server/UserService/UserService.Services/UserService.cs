using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Exceptions;
using UserService.Services.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public  UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AccountModel> GetAccountInfoAsync(Guid accountId)

        {
            AccountModel account=  await _userRepository.GetAccountInfoAsync(accountId);
            if(account==null)
            {
                throw new AccountNotFoundException(accountId);
            }
            return account;
        }

        public async Task<Guid> LoginAsync(string email, string password)
        {
          UserModel user=  await _userRepository.GetUser(email, password);

            if (user == null)
            {
                Log.Information( $"attemt to login for user with email:{email} failed!");
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
