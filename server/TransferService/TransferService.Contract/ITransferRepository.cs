using System;
using System.Threading.Tasks;
using TransferService.Contract.Enums;
using TransferService.Contract.Models;

namespace TransferService.Contract
{
    public  interface ITransferRepository
    {
        Task<TransferModel> Add(TransferModel newTransfer);
        Task UpdateTransferStatus(Guid transferId, bool isTransferSuccess, string failureReason);

    }
}
