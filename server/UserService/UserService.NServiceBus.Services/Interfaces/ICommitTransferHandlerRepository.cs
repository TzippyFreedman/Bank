using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserService.NServiceBus.Services.Interfaces
{
   public interface ICommitTransferHandlerRepository
    {
        Task Pull(Guid srcAccount, float amount);
        Task Push(Guid srcAccount, float amount);
        Task<bool> CheckExists(Guid accountId);
        bool CheckBalance(Guid accountId, float amount);
    }
}
