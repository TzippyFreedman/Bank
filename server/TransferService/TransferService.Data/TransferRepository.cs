using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransferService.Contract;
using TransferService.Contract.Enums;
using TransferService.Contract.Models;
using TransferService.Data.Entities;

namespace TransferService.Data
{
    public class TransferRepository : ITransferRepository
    {
        private readonly IMapper _mapper;
        private readonly TransferDbContext _transferDbContext;

        public TransferRepository(IMapper mapper, TransferDbContext transferDbContext)
        {
            _mapper = mapper;
            _transferDbContext = transferDbContext;
        }

        public async Task<TransferModel> AddAsync(TransferModel transferModel)
        {
            Transfer transfer = _mapper.Map<Transfer>(transferModel);
            _transferDbContext.Transfers.Add(transfer);
            await _transferDbContext.SaveChangesAsync();
            return _mapper.Map<TransferModel>(transfer);
        }

        public async Task UpdateTransferStatusAsync(Guid transferId, bool isTransferSuccess, string failureReason)
        {
            Transfer transferToUpdate = await _transferDbContext.Transfers
                .Where(transfer => transfer.Id == transferId)
                .FirstOrDefaultAsync();
            transferToUpdate.Status = isTransferSuccess ? TransferStatus.Succeeded : TransferStatus.Failed;
            transferToUpdate.FailureReason = failureReason;
        }
    }
}
