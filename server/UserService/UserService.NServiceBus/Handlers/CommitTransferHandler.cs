using Messages.Commands;
using Messages.Messages;
using Newtonsoft.Json.Linq;
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

      private readonly IUserHandlerRepository _commitTransferHandlerRepository;

        public CommitTransferHandler(IUserHandlerRepository commitTransferHandlerRepository)
        {
            _commitTransferHandlerRepository = commitTransferHandlerRepository;

        }
        public async Task Handle(ICommitTransfer message, IMessageHandlerContext context)
        {
            bool isTransferDone = false;
            string comment;
            int amountForTransfer = (int)Math.Round(message.Amount * 100);
            bool isSrcAccountExists = await _commitTransferHandlerRepository.CheckExistsAsync(message.SrcAccountId);
            if (isSrcAccountExists)
            {
                bool isBalanceOK = await _commitTransferHandlerRepository.CheckBalanceAsync(message.SrcAccountId, amountForTransfer);
                if (isBalanceOK == true)
                {
                    await _commitTransferHandlerRepository.DrawAsync(message.SrcAccountId, amountForTransfer);
                    isTransferDone = true;
                }
                else
                {
                    comment = "balance of source account is not enough";
                }
            }

            if (await _commitTransferHandlerRepository.CheckExistsAsync(message.DestAccountId) == true)
            {
                if (await _commitTransferHandlerRepository.CheckBalanceAsync(message.DestAccountId, amountForTransfer) == true)
                {
                    await _commitTransferHandlerRepository.DepositAsync(message.DestAccountId, amountForTransfer);
                    isTransferDone = true;
                }
            }
           
            await context.Reply<ICommitTransferResponse>(message =>
            {
                message.IsTransferCommited = isTransferDone;

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