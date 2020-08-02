using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract.Models;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class AccountService : IAccountService
    {
        public AccountService()
        {

        }
        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            AccountModel account = await _userRepository.GetAccountByIdAsync(accountId);
            return account;
        }
    }
}
