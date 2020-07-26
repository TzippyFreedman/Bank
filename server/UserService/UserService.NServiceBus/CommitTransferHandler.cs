using Messages.Commands;
using Messages.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract;

namespace UserService.NServiceBus
{
    class CommitTransferHandler : IHandleMessages<ICommitTransfer>
    {

        private readonly IUserRepository _userRepository;

        public CommitTransferHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(ICommitTransfer message, IMessageHandlerContext context)
        {
            bool isTransferSucceeded = true;
            string failureReason = null;
            try
            {
                await _userRepository.DrawAsync(message.SrcAccountId, message.Amount);
                await _userRepository.DepositAsync(message.DestAccountId, message.Amount);
            }
            catch (Exception ex)
            {
                isTransferSucceeded = false;
                failureReason = ex.Message;
            }
            finally
            {
              await SendResponse(isTransferSucceeded, failureReason, context);
            }
        }

        private async Task SendResponse(bool isTransferSucceeded, string failureReason, IMessageHandlerContext context)
        {
            await context.Reply<ICommitTransferResponse>(message =>
            {
                message.IsTransferSucceeded = isTransferSucceeded;
                message.FailureReason = failureReason;
            }).ConfigureAwait(false);
        }
    }
}