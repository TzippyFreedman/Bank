using Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransferService.NServiceBus.Services.Interfaces
{
    public interface ITransferHandlerRepository
    {
        Task UpdateTransferStatus(Guid transferId, TransferStatus transferStatus, string failureReason);
    }
}
