using System;

namespace Messages.Events
{
    public interface ITransactionRequestAdded
    {
        Guid TransactionId { get; set; }
        Guid SrcAccountId { get; set; }
        Guid DestAccountId { get; set; }
        DateTime OperationTime { get; set; }

        int Amount { get; set; }
    }
}
