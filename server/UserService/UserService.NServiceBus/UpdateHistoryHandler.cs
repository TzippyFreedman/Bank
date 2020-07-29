using AutoMapper;
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
        private readonly IMapper _mapper;

        public UpdateHistoryHandler(IOperationsHistoryRepository operationsHistoryRepository, IMapper mapper)
        {
            _operationsHistoryRepository = operationsHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(IUpdateHistory message, IMessageHandlerContext context)
        {
            bool isTransactionSucceeded = message.IsTransactionSucceeded;

            await AddHistoryOperation(isTransactionSucceeded, message.SrcBalance, message.SrcAccountId, false, message.FailureReason,message);
            await AddHistoryOperation(isTransactionSucceeded, message.DestBalance, message.DestAccountId, true, message.FailureReason,message);
        }

        private async Task AddHistoryOperation(bool isTransactionSucceeded, int balance, Guid accountId, bool isCredit,string failureReason, IUpdateHistory message)
        {
            HistoryOperationModel historyOperation;

            if (isTransactionSucceeded)
            {
                historyOperation = new SucceededHistoryOperationModel();

                ((SucceededHistoryOperationModel)historyOperation).Balance = balance;
                historyOperation = SetOperationModel(historyOperation,accountId, isCredit, message);

                await _operationsHistoryRepository.AddSucceededOperation((historyOperation as SucceededHistoryOperationModel));
            }
            else
            {
                historyOperation = new FailedHistoryOperationModel();
                ((FailedHistoryOperationModel)historyOperation).FailureReason = failureReason;

                historyOperation = SetOperationModel(historyOperation, accountId, isCredit, message);
                await _operationsHistoryRepository.AddFailedOperation((historyOperation as FailedHistoryOperationModel));
            }
        }

        public HistoryOperationModel SetOperationModel(HistoryOperationModel historyOperation, Guid accountId, bool isCredit, IUpdateHistory message)
        {
            historyOperation.AccountId = accountId;
            historyOperation.TransactionId = message.TransactionId;
            historyOperation.TransactionAmount = message.TransactionAmount;
            historyOperation.OperationTime = message.OperationTime;
            historyOperation.IsCredit = isCredit;
            return historyOperation;
        }

        /*  public async Task Handle(IUpdateHistory message, IMessageHandlerContext context)
          {

              HistoryOperationModel operationSrcAccount = CreateHistoryOperation(message.SrcBalance, message.SrcAccountId, false, message);
              HistoryOperationModel operationDestAccount = CreateHistoryOperation(message.DestBalance, message.DestAccountId, true, message);

              bool isTransactionSucceeded = message.IsTransactionSucceeded;

              if (isTransactionSucceeded)
              {
                  await AddSucceededOperation(operationSrcAccount);
                  await AddSucceededOperation(operationDestAccount);
              }
              else
              {
                  await AddFailedOperation(operationSrcAccount);
                  await AddFailedOperation(operationDestAccount);
              }
          }

          private HistoryOperationModel CreateHistoryOperation(int balance, Guid accountId, bool isCredit, IUpdateHistory message)
          {
              HistoryOperationModel operationModel = new HistoryOperationModel
              {
                  AccountId = accountId,
                  TransactionId = message.TransactionId,
                  TransactionAmount = message.TransactionAmount,
                  OperationTime = message.OperationTime,
                  IsCredit = isCredit,
                  Balance = balance
              };
              return operationModel;
          }
 
        private async Task AddSucceededOperation(HistoryOperationModel operationSrcAccount)
        {
            SucceededHistoryOperationModel succeededOperationSrcAccount = _mapper.Map<SucceededHistoryOperationModel>(operationSrcAccount);
            await _operationsHistoryRepository.AddSucceededOperation(succeededOperationSrcAccount as SucceededHistoryOperationModel);
        }
        private async Task AddFailedOperation(HistoryOperationModel operationSrcAccount)
        {
            FailedHistoryOperationModel failedOperationSrcAccount = _mapper.Map<FailedHistoryOperationModel>(operationSrcAccount);
            await _operationsHistoryRepository.AddFailedOperation(failedOperationSrcAccount as FailedHistoryOperationModel);
        }
         */
    }
}
