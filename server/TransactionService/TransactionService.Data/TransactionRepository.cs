using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransactionService.Contract;
using TransactionService.Contract.Enums;
using TransactionService.Contract.Models;
using TransactionService.Data.Entities;
using TransactionService.Data.Exceptions;

namespace TransactionService.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMapper _mapper;
        private readonly TransactionDbContext _transactionDbContext;

        public TransactionRepository(IMapper mapper, TransactionDbContext transactionDbContext)
        {
            _mapper = mapper;
            _transactionDbContext = transactionDbContext;
        }

        public async Task<TransactionModel> AddAsync(TransactionModel transactionModel)
        {
             Transaction transaction = _mapper.Map< Transaction>(transactionModel);
            _transactionDbContext.Transactions.Add(transaction);
            await _transactionDbContext.SaveChangesAsync();
            return _mapper.Map<TransactionModel>(transaction);
        }

        public async Task<TransactionModel> GetByIdAsync(Guid transactionId)
        {
            Transaction transaction = await _transactionDbContext.Transactions
               .Where(transaction => transaction.Id == transactionId)
               .FirstOrDefaultAsync();
            if (transaction == null)
            {
                throw new TransactionNotFoundException(transactionId);
            }
            return _mapper.Map<TransactionModel>(transaction);
        }

        public async Task UpdateTransactionStatusAsync(Guid transactionId, bool isTransactionSuccess, string failureReason)
        {
             Transaction transactionToUpdate = await _transactionDbContext.Transactions
                .Where(transaction => transaction.Id == transactionId)
                .FirstOrDefaultAsync();
            if (transactionToUpdate == null)
            {
                throw new TransactionNotFoundException(transactionId);
            }
            transactionToUpdate.Status = isTransactionSuccess ? TransactionStatus.Succeeded : TransactionStatus.Failed;
            transactionToUpdate.FailureReason = failureReason;
        }
    }
}
