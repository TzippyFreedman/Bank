using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    public interface ITransferAdded
    {
         Guid TransferId { get; set; }
         Guid FromAccount { get; set; }
         Guid ToAccount   { get; set; }
         float Amount { get; set; }
    }
}
