using Messages.Events;
using NServiceBus;
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
        private readonly IMessageSession _messageSession;
        public TransactionService(ITransactionRepository transactionRepository, IMessageSession messageSession
)
        {
            _transactionRepository = transactionRepository;
            _messageSession = messageSession;
        }
        public async Task AddAsync(TransactionModel transaction)
        {
            TransactionModel newTransaction = await _transactionRepository.AddAsync(transaction);
            await _messageSession.Publish<ITransactionRequestAdded>(message =>
            {
                message.TransactionId = newTransaction.Id;
                message.Amount = newTransaction.Amount;
                message.SrcAccountId = newTransaction.SrcAccountId;
                message.DestAccountId = newTransaction.DestAccountId;
                message.OperationTime = newTransaction.Date;
            });
        }

        public async Task<TransactionModel> GetByIdAsync(Guid transactionId)
        {
            TransactionModel transaction = await _transactionRepository.GetByIdAsync(transactionId);
            return transaction;
        }
    }
}
