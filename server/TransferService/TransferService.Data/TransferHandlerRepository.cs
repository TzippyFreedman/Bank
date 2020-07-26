using Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        async Task UpdateTransferStatus(Guid transferId, TransferStatus transferStatus, string failureReason)
        {
            Transfer transferToUpdate = await _transferDbContext.Transfers
                .Where(transfer => transfer.Id == transferId)
                .FirstOrDefaultAsync();

            transferToUpdate.Status = transferStatus;
            transferToUpdate.FailureReason = failureReason;
        }
    }
}
