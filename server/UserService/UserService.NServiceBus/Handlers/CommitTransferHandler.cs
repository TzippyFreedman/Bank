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
            bool isTransferDone = true;
            string comment = null;
            int amountForTransfer = (int)Math.Round(message.Amount * 100);

            bool isSrcAccountExists = await _committransferHandlerRepository.CheckExistsAsync(message.SrcAccountId);
            if (isSrcAccountExists == true)
            {
                bool isBalanceOK = await _committransferHandlerRepository.CheckBalanceAsync(message.SrcAccountId, amountForTransfer);
                if (isBalanceOK == true)
                {
                    await _committransferHandlerRepository.DrawAsync(message.SrcAccountId, amountForTransfer);
                    bool isDestAccountExists = await _committransferHandlerRepository.CheckExistsAsync(message.DestAccountId)
                    if (isDestAccountExists == true)
                    {

                        await _committransferHandlerRepository.DepositAsync(message.DestAccountId, amountForTransfer);
                    }
                    else
                    {
                        comment = "destination account doesnt exist.";
                        isTransferDone = false;

                    }
                }
                else
                {
                    comment = "balance in source account is too low.";
                    isTransferDone = false;

                }

            }
            else
            {
                comment = "source account doesnt exist.";
                isTransferDone = false;

            }



            await context.Reply<ICommitTransferResponse>(message =>
            {
                message.IsTransferSucceeded = isTransferDone;
                message.FailureReason = comment;
            });
            //await context.Publish<IMoneyAdded>(command =>
            //{
            //    command.TransferId = message.TransferId;
            //    command.Amount = message.Amount;

            //});
            //  }
        }
    }
}