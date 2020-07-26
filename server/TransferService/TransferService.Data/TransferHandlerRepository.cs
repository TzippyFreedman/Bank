using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransferService.Data.Entities;

namespace TransferService.Data
{
    public class TransferHandlerRepository
    {
        private readonly TransferDbContext _transferDbContext;

        public TransferHandlerRepository(TransferDbContext transferDbContext)
        {
            _transferDbContext = transferDbContext;
        }
        public async Task UpdateTransferStatus(Guid transferId, TransferStatus transferStatus, string failureReason)
        {
            Transfer transferToUpdate = await _transferDbContext.Transfers
                .Where(transfer => transfer.Id == transferId)
                .FirstOrDefaultAsync();

            transferToUpdate.Status = transferStatus;
            transferToUpdate.FailureReason = failureReason;
        }
    }
}
