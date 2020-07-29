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

            await AddOperation(isTransactionSucceeded, message.SrcBalance, message.SrcAccountId, false, message.FailureReason,message);
            await AddOperation(isTransactionSucceeded, message.DestBalance, message.DestAccountId, true, message.FailureReason,message);
        }

        private async Task AddOperation(bool isTransactionSucceeded, int balance, Guid accountId, bool isCredit,string failureReason, IUpdateHistory message)
        {
            OperationModel operation;

            if (isTransactionSucceeded)
            {
                operation = new SucceededOperationModel();

                ((SucceededOperationModel)operation).Balance = balance;
                operation = SetOperationModel(operation,accountId, isCredit, message);

                await _operationsHistoryRepository.AddSucceededOperation((operation as SucceededOperationModel));
            }
            else
            {
                operation = new FailedOperationModel();
                ((FailedOperationModel)operation).FailureReason = failureReason;

                operation = SetOperationModel(operation, accountId, isCredit, message);
                await _operationsHistoryRepository.AddFailedOperation((operation as FailedOperationModel));
            }
        }

        public OperationModel SetOperationModel(OperationModel operation, Guid accountId, bool isCredit, IUpdateHistory message)
        {
            operation.AccountId = accountId;
            operation.TransactionId = message.TransactionId;
            operation.TransactionAmount = message.TransactionAmount;
            operation.OperationTime = message.OperationTime;
            operation.IsCredit = isCredit;
            return operation;
        }

        /*  public async Task Handle(IUpdateHistory message, IMessageHandlerContext context)
          {

              OperationModel operationSrcAccount = CreateOperation(message.SrcBalance, message.SrcAccountId, false, message);
              OperationModel operationDestAccount = CreateOperation(message.DestBalance, message.DestAccountId, true, message);

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

          private OperationModel CreateOperation(int balance, Guid accountId, bool isCredit, IUpdateHistory message)
          {
              OperationModel operationModel = new OperationModel
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
 
        private async Task AddSucceededOperation(OperationModel operationSrcAccount)
        {
            SucceededOperationModel succeededOperationSrcAccount = _mapper.Map<SucceededOperationModel>(operationSrcAccount);
            await _operationsHistoryRepository.AddSucceededOperation(succeededOperationSrcAccount as SucceededOperationModel);
        }
        private async Task AddFailedOperation(OperationModel operationSrcAccount)
        {
            FailedOperationModel failedOperationSrcAccount = _mapper.Map<FailedOperationModel>(operationSrcAccount);
            await _operationsHistoryRepository.AddFailedOperation(failedOperationSrcAccount as FailedOperationModel);
        }
         */
    }
}
