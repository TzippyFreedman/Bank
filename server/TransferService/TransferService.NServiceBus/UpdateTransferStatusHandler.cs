using Messages.Commands;
using NServiceBus;
using System.Threading.Tasks;
using TransferService.Contract;

namespace TransferService.NServiceBus
{
    public class UpdateTransferStatusHandler : IHandleMessages<IUpdateTransferStatus>
    {

        private readonly ITransferRepository _transferHandlerRepository;

        public UpdateTransferStatusHandler(ITransferRepository transferHandlerRepository)
        {
            _transferHandlerRepository = transferHandlerRepository;
        }
        public async Task Handle(IUpdateTransferStatus message, IMessageHandlerContext context)
        {
            await _transferHandlerRepository.UpdateTransferStatusAsync(message.TransferId, message.IsTransferSucceeded, message.FailureReason)
                .ConfigureAwait(false);
        }
    }
}

