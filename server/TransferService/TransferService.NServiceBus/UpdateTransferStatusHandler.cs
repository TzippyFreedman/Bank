using Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferService.NServiceBus.Services.Interfaces;

namespace TransferService.NServiceBus
{
    public class UpdateTransferStatusHandler : IHandleMessages<IUpdateTransferStatus>
    {
        private readonly ITransferHandlerRepository _transferHandlerRepository;

        public UpdateTransferStatusHandler(ITransferHandlerRepository transferHandlerRepository)
        {
            _transferHandlerRepository = transferHandlerRepository;
        }
        public async Task Handle(IUpdateTransferStatus message, IMessageHandlerContext context)
        {
            await _transferHandlerRepository.UpdateTransferStatus(message.TransferId, message.TransferStatus, message.FailureReason); 
        }
        
     }
}
