using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserService.NServiceBus.Services.Interfaces
{
   public interface IUserHandlerRepository
    {
        Task DrawAsync(Guid srcAccount, int amount);
        Task DepositAsync(Guid destAccount, int amount);
        Task<bool> IsExistsAsync(Guid accountId);
        Task<bool> IsBalanceOkAsync(Guid accountId, int amount);
    }
}
