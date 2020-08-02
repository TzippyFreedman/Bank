using System;
using System.Threading.Tasks;
using TransactionService.Contract.Models;

namespace TransactionService.Services.Interfaces
{
    public interface ITransactionService
    {
        Task AddAsync(TransactionModel newTransaction);
        Task<TransactionModel> GetByIdAsync(Guid transactionId);
    }
}
