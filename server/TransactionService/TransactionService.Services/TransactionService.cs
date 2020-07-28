using System;
using System.Threading.Tasks;
using TransactionService.Contract;
using TransactionService.Contract.Models;
using TransactionService.Services.Interfaces;

namespace TransactionService.Services
{
    public class TransactionService : ITransactionService
    { 
    
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<TransactionModel> AddAsync(TransactionModel transaction)
        {
            TransactionModel newTransaction = await _transactionRepository.AddAsync(transaction);
            return newTransaction;
        }

        public async Task<TransactionModel> GetByIdAsync(Guid transactionId)
        {
            TransactionModel transaction = await _transactionRepository.GetByIdAsync(transactionId);
            return transaction;
        }
    }
}
