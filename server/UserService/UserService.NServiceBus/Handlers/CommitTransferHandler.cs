using Messages.Commands;
using Messages.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.NServiceBus.Services.Interfaces;

namespace UserService.NServiceBus.Handlers
{
    class CommitTransferHandler : IHandleMessages<ICommitTransfer>
    {
        //private readonly IUserService _userService;

      private readonly IUserHandlerRepository _committransferHandlerRepository;

        public CommitTransferHandler(IUserHandlerRepository committransferHandlerRepository)
        {
            _committransferHandlerRepository = committransferHandlerRepository;

        }
        public async Task Handle(ICommitTransfer message, IMessageHandlerContext context)
        {
            bool isTransferSucceeded = true;
            string comment = null;
            int amountToTransfer = (int)Math.Round(message.Amount * 100);
            bool isSrcAccountExists = await _committransferHandlerRepository.CheckExistsAsync(message.SrcAccountId);
            if (isSrcAccountExists == false)
            {
                comment = "source account doesnt exist.";
                isTransferSucceeded = false; 
            }
            else
            {
                bool isDestAccountExists = await _committransferHandlerRepository.CheckExistsAsync(message.DestAccountId);
                if (isDestAccountExists == false)
                {
                    comment = "destination account doesnt exist.";
                    isTransferSucceeded = false;
                }
                else
                {
                    bool isBalanceOK = await _committransferHandlerRepository.CheckBalanceAsync(message.SrcAccountId, amountToTransfer);
                    if (isBalanceOK == false)
                    {
                        comment = "balance is too low.";
                        isTransferSucceeded = false;
                    }
                    else
                    {
                        await _committransferHandlerRepository.DrawAsync(message.SrcAccountId, amountToTransfer);
                        await _committransferHandlerRepository.DepositAsync(message.DestAccountId, amountToTransfer);
                    }
                }
            }


            await context.Reply<ICommitTransferResponse>(message =>
            {
                message.IsTransferSucceeded = isTransferSucceeded;
                message.FailureReason = comment;
            });

        }
    }
}