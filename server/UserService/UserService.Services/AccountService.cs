using System;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountModel> GetByIdAsync(Guid accountId)
        {
            AccountModel account = await _accountRepository.GetByIdAsync(accountId);
            return account;
        }
    }
}
