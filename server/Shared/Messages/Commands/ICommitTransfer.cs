using System;

namespace Messages.Commands
{
    public interface ICommitTransfer
    {
        Guid TransferId { get; set; }
        Guid SrcAccountId { get; set; }
        Guid DestAccountId { get; set; }
        int Amount { get; set; }
    }
}
