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
            try
            {
                await _userRepository.DrawAsync(message.SrcAccountId, message.Amount);
                await _userRepository.DepositAsync(message.DestAccountId, message.Amount);
            }
            catch (Exception ex) when (ex is DataNotFoundException || ex is InsufficientBalanceForTransactionException)
            {
                isTransactionSucceeded = false;
                failureReason = ex.Message;
            }
            finally
            {
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