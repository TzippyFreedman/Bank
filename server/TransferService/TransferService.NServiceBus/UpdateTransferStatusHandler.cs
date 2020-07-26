using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransferService.NServiceBus
{
    public class UpdateTransferStatusHandler : IHandleMessages<IUpdateTransferStatus>
    {
        public Task Handle(IUpdateTransferStatus message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
