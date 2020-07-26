using System;
using System.Threading.Tasks;
using TransferService.Contract.Enums;
using TransferService.Contract.Models;

namespace TransferService.Contract
{
    public  interface ITransferRepository
    {
        Task<TransferModel> AddAsync(TransferModel newTransfer);
        Task UpdateTransferStatusAsync(Guid transferId, bool isTransferSuccess, string failureReason);

    }
}
