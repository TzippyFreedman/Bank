using System;

namespace Messages.Commands
{
    public interface ICommitTransaction
    {
        Guid TransactionId { get; set; }
        Guid SrcAccountId { get; set; }
        Guid DestAccountId { get; set; }
        int Amount { get; set; }
        DateTime OperationTime { get; set; }

    }
}
