using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
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
            bool IsTransactionSucceeded = message.IsTransactionSucceeded;
            if (IsTransactionSucceeded==true)
            {
            HistoryOperationModel historyOperationModel = new HistoryOperationModel();
            historyOperationModel.OperationTime = message.OperationTime;
            historyOperationModel.AccountId = message.SrcAccountId;
            historyOperationModel.Balance = message.SrcBalance;
            historyOperationModel.IsCredit = false;
            await _operationsHistoryRepository.AddSuccessedOperation(historyOperationModel);
            historyOperationModel.AccountId = message.DestAccountId;
            historyOperationModel.Balance = message.DestBalance;
            historyOperationModel.IsCredit = true;
            await _operationsHistoryRepository.AddSuccessedOperation(historyOperationModel);
            }
            else
            {
                
                    FailedHistoryOperationModel historyOperationModel = new FailedHistoryOperationModel();
                    historyOperationModel.OperationTime = message.OperationTime;
                    historyOperationModel.AccountId = message.SrcAccountId;
                    historyOperationModel.IsCredit = false;
                    await _operationsHistoryRepository.AddFailedOperation(historyOperationModel);
                    historyOperationModel.AccountId = message.DestAccountId;
                    historyOperationModel.IsCredit = true;
                    await _operationsHistoryRepository.AddFailedOperation(historyOperationModel);
               
            }

        }
    }
}
