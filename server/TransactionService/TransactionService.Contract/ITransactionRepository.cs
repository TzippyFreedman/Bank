using System;
using System.Threading.Tasks;
using TransactionService.Contract.Models;

namespace TransactionService.Contract
{
    public interface ITransactionRepository
    {
        Task<TransactionModel> AddAsync(TransactionModel newTransaction);
        Task UpdateTransactionStatusAsync(Guid transactionId, bool isTransactionSuccess, string failureReason);
    }
}
