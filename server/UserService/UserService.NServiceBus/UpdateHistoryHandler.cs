using Messages.Commands;
using NServiceBus;
using System;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;

namespace UserService.NServiceBus
{
    public class UpdateHistoryHandler : IHandleMessages<IUpdateHistory>
    {
        private readonly IOperationsHistoryRepository _operationsHistoryRepository;

        public UpdateHistoryHandler(IOperationsHistoryRepository operationsHistoryRepository)
        {
            _operationsHistoryRepository = operationsHistoryRepository;
        }

        public async Task Handle(IUpdateHistory message, IMessageHandlerContext context)
        {
            bool isTransactionSucceeded = message.IsTransactionSucceeded;

            await AddHistoryOperation(isTransactionSucceeded, message.SrcBalance, message.SrcAccountId, false, message);
            await AddHistoryOperation(isTransactionSucceeded, message.DestBalance, message.DestAccountId, true, message);
        }

        private async Task AddHistoryOperation(bool isTransactionSucceeded, int balance, Guid accountId, bool isCredit, IUpdateHistory message)
        {
            HistoryOperationModel historyOperation;
            if (isTransactionSucceeded)
            {
                historyOperation = new SucceededHistoryOperationModel();
                ((SucceededHistoryOperationModel)historyOperation).Balance = balance;
            }
            else
            {
                historyOperation = new FailedHistoryOperationModel();
            }

            historyOperation.AccountId = accountId;
            historyOperation.TransactionId = message.TransactionId;
            historyOperation.TransactionAmount = message.TransactionAmount;
            historyOperation.OperationTime = message.OperationTime;
            historyOperation.IsCredit = isCredit;

            if (historyOperation is SucceededHistoryOperationModel)
            {
                await _operationsHistoryRepository.AddSuccessedOperation((historyOperation as SucceededHistoryOperationModel));
            }
            else
            {
                await _operationsHistoryRepository.AddFailedOperation((historyOperation as FailedHistoryOperationModel));
            }
        }
    }
}
