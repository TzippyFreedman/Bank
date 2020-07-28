using Messages.Commands;
using NServiceBus;
using System.Threading.Tasks;
using TransactionService.Contract;

namespace TransactionService.NServiceBus
{
    public class UpdateTransactionStatusHandler : IHandleMessages<IUpdateTransactionStatus>
    {

        private readonly ITransactionRepository _transactionHandlerRepository;

        public UpdateTransactionStatusHandler(ITransactionRepository transactionHandlerRepository)
        {
            _transactionHandlerRepository = transactionHandlerRepository;
        }
        public async Task Handle(IUpdateTransactionStatus message, IMessageHandlerContext context)
        {
            await _transactionHandlerRepository.UpdateTransactionStatusAsync(message.TransactionId, message.IsTransactionSucceeded, message.FailureReason);               
        }
    }
}

