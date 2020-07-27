using System;

namespace Messages.Events
{
    public interface ITransferRequestAdded
    {
        Guid TransferId { get; set; }
        Guid SrcAccountId { get; set; }
        Guid DestAccountId { get; set; }
        int Amount { get; set; }
    }
}
