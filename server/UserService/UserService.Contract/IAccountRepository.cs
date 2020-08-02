using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract
{
   public interface IAccountRepository
    {
        Task<AccountModel> GetByUserIdAsync(Guid userId);
        Task<AccountModel> GetByIdAsync(Guid accountId);
        Task<int> WithDrawAsync(Guid srcAccount, int amount);
        Task<int> DepositAsync(Guid destAccount, int amount);
        Task<bool> IsExistsAsync(Guid accountId);
        Task<bool> IsBalanceOkAsync(Guid accountId, int amount);

    }
}
