using Messages.Commands;
using Messages.Messages;
using NServiceBus;
using System;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Data.Exceptions;

namespace UserService.NServiceBus
{
    class CommitTransactionHandler : IHandleMessages<ICommitTransaction>
    {
        private readonly IAccountRepository _accountRepository;

        public CommitTransactionHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task Handle(ICommitTransaction message, IMessageHandlerContext context)
        {
            bool isTransactionSucceeded = true;
            string failureReason = null;
            int destAccountBalance=0;
            int srcAccountBalance=0;

            try
            {
               srcAccountBalance=  await _accountRepository.WithDrawAsync(message.SrcAccountId, message.Amount);
               destAccountBalance=  await _accountRepository.DepositAsync(message.DestAccountId, message.Amount);          
            }
            catch (Exception ex) when (ex is DataNotFoundException || ex is InsufficientBalanceForTransactionException)
            {
                isTransactionSucceeded = false;
                failureReason = ex.Message;              
            }
            finally
            {
                //implementation
                await context.SendLocal<IUpdateHistory>(command =>
                {
                    command.IsTransactionSucceeded = isTransactionSucceeded;
                    command.TransactionId = message.TransactionId;
                    command.TransactionAmount = message.Amount;
                    command.SrcAccountId = message.SrcAccountId;
                    command.DestAccountId = message.DestAccountId;
                    command.SrcBalance = srcAccountBalance;
                    command.DestBalance = destAccountBalance;
                    command.OperationTime = message.OperationTime;
                    command.FailureReason = failureReason;

                });
                await SendResponse(isTransactionSucceeded, failureReason, context);
            }
        }

        private async Task SendResponse(bool isTransactionSucceeded, string failureReason, IMessageHandlerContext context)
        {
            await context.Reply<ICommitTransactionResponse>(message =>
            {
                message.IsTransactionSucceeded = isTransactionSucceeded;
                message.FailureReason = failureReason;
            });
        }
    }
}