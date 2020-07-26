using Messages.Commands;
using Messages.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
          //  int amount = int(message.Amount * 100);
            if (await _committransferHandlerRepository.CheckExists(message.SrcAccountId) == true)
            {
                if (_committransferHandlerRepository.CheckBalance(message.SrcAccountId, message.Amount) == true)
                {
                    await _committransferHandlerRepository.Pull(message.SrcAccountId, message.Amount);
                    isTransferDone = true;
                }
            }

            if (await _committransferHandlerRepository.CheckExists(message.DestAccountId) == true)
            {
                if (_committransferHandlerRepository.CheckBalance(message.DestAccountId, message.Amount) == true)
                {
                    await _committransferHandlerRepository.Push(message.DestAccountId, message.Amount);
                    isTransferDone = true;
                }
            }
           // await _userService.AddMoney(message.AccountId, message.Amount);
            //    UserAccount userDestAccount = await _userDbContext.UserFiles.Where(u => u.UserId == message.SrcAccountId).FirstOrDefaultAsync();
            //if (userDestAccount != null)
            //{
            //    userDestAccount.Balance += message.Amount;

            //    // throw new AccountDoesntExistException(account);
            //}
            //    else
            //    {
            //        isTransferDone = false;
            //    }
            //UserAccount userSrcAccount = await _userDbContext.UserFiles.Where(u => u.UserId == message.DestAccountId).FirstOrDefaultAsync();
            //if (userSrcAccount != null)
            //{
            //    userSrcAccount.Balance -= message.Amount;

            //    // throw new AccountDoesntExistException(account);
            //}
            //    else
            //    {
            //        isTransferDone = false;
            //    }
            //bool  AccountExists = false;
            //  //check if account x exists
            //  if (_userService.CheckExists(message.AccountId))
            //  {
            //      AccountExists = true;
            //  }

            //  else
            //  {
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