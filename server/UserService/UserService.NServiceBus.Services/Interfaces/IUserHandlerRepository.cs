using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserService.NServiceBus.Services.Interfaces
{
   public interface IUserHandlerRepository
    {
        Task Draw(Guid srcAccount, int amount);
        Task Deposit(Guid destAccount, int amount);
        Task<bool> CheckExists(Guid accountId);
        bool CheckBalance(Guid accountId, int amount);
    }
}
