using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    public interface ITransferRequestAdded
    {
         Guid TransferId { get; set; }
         Guid SrcAccountId { get; set; }
         Guid DestAccountId   { get; set; }
         float Amount { get; set; }
    }
}
