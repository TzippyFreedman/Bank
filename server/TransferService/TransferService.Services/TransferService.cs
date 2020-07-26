using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferService.Contract;
using TransferService.Contract.Models;
using TransferService.Services.Interfaces;

namespace TransferService.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;

        public TransferService(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }
        public async Task<TransferModel> Add(TransferModel transfer)
        {
         TransferModel newTransfer =   await _transferRepository.Add(transfer);
            return newTransfer;
        }
    }
}
