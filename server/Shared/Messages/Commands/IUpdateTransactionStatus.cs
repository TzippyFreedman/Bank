using System;

namespace Messages.Commands
{
    public interface IUpdateTransactionStatus
    {
        Guid TransactionId { get; set; }
        bool IsTransactionSucceeded { get; set; }
        public string FailureReason { get; set; }
    }
}
