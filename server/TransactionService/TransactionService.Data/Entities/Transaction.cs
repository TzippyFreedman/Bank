using System;
using TransactionService.Contract.Enums;

namespace TransactionService.Data.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid SrcAccountId { get; set; }
        public Guid DestAccountId { get; set; }
        public int Amount { get; set; }
        public TransactionStatus Status { get; set; }
        public string FailureReason { get; set; }
        public DateTime Date { get; set; }
    }
}
