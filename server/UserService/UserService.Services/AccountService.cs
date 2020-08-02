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
        private readonly IAccountService _accountService;

        public AccountService(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<AccountModel> GetByIdAsync(Guid accountId)
        {
            AccountModel account = await _accountService.GetByIdAsync(accountId);
            return account;
        }
    }
}
