using Messages.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransferService.NServiceBus
{
    public class TransferPolicySaga : Saga<TransferPolicySagaData>,
               IAmStartedByMessages<ITransferRequestAdded>

    {
        public async Task Handle(ITransferRequestAdded message, IMessageHandlerContext context)
        {
           // context.Send()
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

