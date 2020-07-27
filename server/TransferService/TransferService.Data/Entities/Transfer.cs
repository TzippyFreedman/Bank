using System;
using TransferService.Contract.Enums;

namespace TransferService.Data.Entities
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public Guid SrcAccountId { get; set; }
        public Guid DestAccountId { get; set; }
        public int Amount { get; set; }
        public TransferStatus Status { get; set; }
        public string FailureReason { get; set; }
        public DateTime Date { get; set; }
    }
}
