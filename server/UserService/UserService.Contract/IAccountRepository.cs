using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract
{
   public interface IAccountRepository
    {
        Task<AccountModel> GetAccountByUserIdAsync(Guid userId);
        Task<AccountModel> GetAccountByIdAsync(Guid accountId);
        Task<int> DrawAsync(Guid srcAccount, int amount);
        Task<int> DepositAsync(Guid destAccount, int amount);
        Task<bool> IsExistsAsync(Guid accountId);
        Task<bool> IsBalanceOkAsync(Guid accountId, int amount);

    }
}
