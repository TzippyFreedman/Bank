using System;
using TransferService.Contract.Enums;

namespace TransferService.Data.Entities
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public Guid SrcAccount { get; set; }
        public Guid DestAccount { get; set; }
        public int Amount { get; set; }
        public TransferStatus Status { get; set; }
        public string FailureReason { get; set; }
    }
}
