using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Data.Entities;
using UserService.Data.Exceptions;

namespace UserService.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public AccountRepository(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }
        public async Task<AccountModel> GetByUserIdAsync(Guid accountId)
        {
            Account account = await _userDbContext.Accounts
                      .Where(file => file.UserId == accountId)
                      .FirstOrDefaultAsync();
            if (account == null)
            {

                throw new AccountNotFoundException(accountId);
            }
            return _mapper.Map<AccountModel>(account);
        }

        public async Task<AccountModel> GetByIdAsync(Guid accountId)
        {
            Account account = await _userDbContext.Accounts
                  .Where(account => account.Id == accountId)
                  .FirstOrDefaultAsync();
            if (account == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            return _mapper.Map<AccountModel>(account);
        }
        public async Task<bool> IsBalanceOkAsync(Guid accountId, int amount)
        {
            Account user = await _userDbContext.Accounts
                .Where(u => u.Id == accountId)
                .FirstOrDefaultAsync();
            bool isBalanceOK = user.Balance >= amount ? true : false;
            return isBalanceOK;
        }

        public async Task<bool> IsExistsAsync(Guid accountId)
        {
            return await _userDbContext.Accounts.AnyAsync(u => u.Id == accountId);
        }

        public async Task<int> WithDrawAsync(Guid accountId, int amount)
        {
            Account userAccount = await _userDbContext.Accounts
                .Where(u => u.Id == accountId)
                .FirstOrDefaultAsync();
            if (userAccount == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            if (userAccount.Balance < amount)
            {
                throw new InsufficientBalanceForTransactionException(accountId, amount);
            }
            userAccount.Balance -= amount;
            return userAccount.Balance;

        }

        public async Task<int> DepositAsync(Guid accountId, int amount)
        {
            Account userAccount = await _userDbContext.Accounts
                .Where(u => u.Id == accountId)
                .FirstOrDefaultAsync();
            if (userAccount == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            userAccount.Balance += amount;
            return userAccount.Balance;
        }

    }
}
