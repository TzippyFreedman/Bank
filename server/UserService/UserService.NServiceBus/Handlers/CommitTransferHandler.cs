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
            bool isTransferDone = false;
            int amountForTransfer = (int)Math.Round(message.Amount * 100);
            if (await _committransferHandlerRepository.CheckExists(message.SrcAccountId) == true)
            {
                if (_committransferHandlerRepository.CheckBalance(message.SrcAccountId, amountForTransfer) == true)
                {
                    await _committransferHandlerRepository.Draw(message.SrcAccountId, amountForTransfer);
                    isTransferDone = true;
                }
            }

            if (await _committransferHandlerRepository.CheckExists(message.DestAccountId) == true)
            {
                if (_committransferHandlerRepository.CheckBalance(message.DestAccountId, amountForTransfer) == true)
                {
                    await _committransferHandlerRepository.Deposit(message.DestAccountId, amountForTransfer);
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