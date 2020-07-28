using Messages.Commands;
using NServiceBus;
using System.Reflection.Metadata.Ecma335;
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
            if (isTransactionSucceeded == true)
            {
                HistoryOperationModel historyOperationModel = new HistoryOperationModel();
                historyOperationModel.Balance = message.SrcBalance;
            }
            else
            {
                FailedHistoryOperationModel historyOperationModel = new FailedHistoryOperationModel();
                SetHistoryOperation(historyOperationModel)
            }
            /*
                            historyOperationModel.TransactionId = message.TransactionId;
                            historyOperationModel.TransactionAmount = message.TransactionAmount;
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

                            historyOperationModel.TransactionId = message.TransactionId;
                            historyOperationModel.TransactionAmount = message.TransactionAmount;
                            historyOperationModel.OperationTime = message.OperationTime;
                            historyOperationModel.AccountId = message.SrcAccountId;
                            historyOperationModel.IsCredit = false;

                            await _operationsHistoryRepository.AddFailedOperation(historyOperationModel);

                            historyOperationModel.AccountId = message.DestAccountId;
                            historyOperationModel.IsCredit = true;

                            await _operationsHistoryRepository.AddFailedOperation(historyOperationModel);

                        }*/

        }

        private void SetHistoryOperation(HistoryOperationModel historyOperation, bool isSuccess, IUpdateHistory message)
        {
            historyOperation.TransactionId = message.TransactionId;
            historyOperation.TransactionAmount = message.TransactionAmount;
            historyOperation.OperationTime = message.OperationTime;

        }
    }
}
