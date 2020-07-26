using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Commands
{
    public interface IUpdateTransferStatus
    {
        Guid TransferId { get; set; }
        TransferStatus TransferStatus { get; set; }
        public string FailureReason { get; set; }
    }
}
