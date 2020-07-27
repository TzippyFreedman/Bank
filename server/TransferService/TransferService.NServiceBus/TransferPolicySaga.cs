using Messages.Commands;
using Messages.Events;
using Messages.Messages;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace TransferService.NServiceBus
{
    public class TransferPolicySaga : Saga<TransferPolicySagaData>,
               IAmStartedByMessages<ITransferRequestAdded>,
        IHandleMessages<ICommitTransferResponse>
    {
        public async Task Handle(ITransferRequestAdded message, IMessageHandlerContext context)
        {
            await context.Send<ICommitTransfer>(msg =>
             {
                 msg.TransferId = message.TransferId;
                 msg.SrcAccountId = message.SrcAccountId;
                 msg.DestAccountId = message.DestAccountId;
                 msg.Amount = message.Amount;
             })
                .ConfigureAwait(false);
        }

        public async Task Handle(ICommitTransferResponse message, IMessageHandlerContext context)
        {
            await context.SendLocal<IUpdateTransferStatus>(msg =>
             {
                 msg.TransferId = Data.TransferId;
                 msg.IsTransferSucceeded = message.IsTransferSucceeded;
                 msg.FailureReason = message.FailureReason;
             })
                 .ConfigureAwait(false);
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransferPolicySagaData> mapper)
        {
            mapper.ConfigureMapping<ITransferRequestAdded>(message => message.TransferId)
             .ToSaga(sagaData => sagaData.TransferId);
        }
    }


    public class TransferPolicySagaData : ContainSagaData
    {
        public Guid TransferId { get; set; }
    }
}

