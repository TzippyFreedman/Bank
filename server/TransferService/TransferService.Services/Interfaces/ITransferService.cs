using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferService.Contract.Models;

namespace TransferService.Services.Interfaces
{
   public interface ITransferService
    {
        Task<TransferModel> AddAsync(TransferModel newTransfer);
    }
}
