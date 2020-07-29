using Messages.Commands;
using Messages.Events;
using Messages.Messages;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace TransactionService.NServiceBus
{
    public class TransactionPolicySaga : Saga<TransactionPolicySagaData>,
               IAmStartedByMessages<ITransactionRequestAdded>,
        IHandleMessages<ICommitTransactionResponse>
    {
        public async Task Handle(ITransactionRequestAdded message, IMessageHandlerContext context)
        {
            //change to command
            await context.Send<ICommitTransaction>(msg =>
             {
                 msg.TransactionId = message.TransactionId;
                 msg.OperationTime = message.OperationTime;
                 msg.SrcAccountId = message.SrcAccountId;
                 msg.DestAccountId = message.DestAccountId;
                 msg.Amount = message.Amount;
             });
        }

        public async Task Handle(ICommitTransactionResponse message, IMessageHandlerContext context)
        {
            await context.SendLocal<IUpdateTransactionStatus>(msg =>
             {
                 msg.TransactionId = Data.TransactionId;
                 msg.IsTransactionSucceeded = message.IsTransactionSucceeded;
                 msg.FailureReason = message.FailureReason;
             });
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicySagaData> mapper)
        {
            mapper.ConfigureMapping<ITransactionRequestAdded>(message => message.TransactionId)
             .ToSaga(sagaData => sagaData.TransactionId);
        }
    }


    public class TransactionPolicySagaData : ContainSagaData
    {
        public Guid TransactionId { get; set; }
    }
}

