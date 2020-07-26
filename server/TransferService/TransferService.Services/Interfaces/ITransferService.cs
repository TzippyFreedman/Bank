using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferService.Services.Models;

namespace TransferService.Services.Interfaces
{
   public interface ITransferService
    {
        Task<TransferModel> Add(TransferModel newTransfer);
    }
}
