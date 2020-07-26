using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferService.Data.Entities;
using TransferService.Services.Interfaces;
using TransferService.Services.Models;

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

        public async Task<TransferModel> Add(TransferModel transferModel)
        {
            Transfer transfer = _mapper.Map<Transfer>(transferModel);
            _transferDbContext.Transfers.Add(transfer);
            await _transferDbContext.SaveChangesAsync();
            return _mapper.Map<TransferModel>(transfer);
        }
    }
}
