using System;

namespace Messages.Commands
{
    public interface IUpdateTransferStatus
    {
        Guid TransferId { get; set; }
        bool IsTransferSucceeded { get; set; }
        public string FailureReason { get; set; }
    }
}
