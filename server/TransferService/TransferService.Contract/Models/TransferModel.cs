using System;
using TransferService.Contract.Enums;

namespace TransferService.Contract.Models
{
    public class TransferModel
    {
        public Guid Id { get; set; }
        public TransferStatus Status { get; set; }
        public Guid SrcAccountId { get; set; }
        public Guid DestAccountId { get; set; }
        public float Amount { get; set; }
        public string FailureReason { get; set; }
    }
}
