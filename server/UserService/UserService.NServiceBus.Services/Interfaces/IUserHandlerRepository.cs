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
        Task<bool> CheckExistsAsync(Guid accountId);
        Task<bool> CheckBalanceAsync(Guid accountId, int amount);
    }
}
