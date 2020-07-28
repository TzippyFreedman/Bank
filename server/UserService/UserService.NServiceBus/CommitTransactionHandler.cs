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
        private readonly IUserRepository _userRepository;

        public CommitTransactionHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(ICommitTransaction message, IMessageHandlerContext context)
        {
            bool isTransactionSucceeded = true;
            string failureReason = null;
            int destAccountBalance=0;
            int srcAccountBalance=0;
            try
            {
               srcAccountBalance=  await _userRepository.DrawAsync(message.SrcAccountId, message.Amount);
               destAccountBalance=  await _userRepository.DepositAsync(message.DestAccountId, message.Amount);
              
            }
            catch (Exception ex) when (ex is DataNotFoundException || ex is InsufficientBalanceForTransactionException)
            {
                isTransactionSucceeded = false;
                failureReason = ex.Message;
               
            }
            finally
            {
                await context.SendLocal<IUpdateHistory>(command =>
                {
                    command.IsTransactionSucceeded = isTransactionSucceeded;
                    command.TransactionId = message.TransactionId;
                    command.TransactionAmount = message.Amount;
                    command.SrcAccountId = message.SrcAccountId;
                    command.DestAccountId = message.DestAccountId;
                    command.SrcBalance = srcAccountBalance;
                    command.DestBalance = srcAccountBalance;
                    command.OperationTime = message.OperationTime;

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