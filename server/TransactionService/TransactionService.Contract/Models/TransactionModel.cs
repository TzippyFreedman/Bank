using System;
using TransactionService.Contract.Enums;

namespace TransactionService.Contract.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public TransactionStatus Status { get; set; }
        public Guid SrcAccountId { get; set; }
        public Guid DestAccountId { get; set; }
        public DateTime Date { get; set; }

        public int Amount { get; set; }
        public string FailureReason { get; set; }
    }
}
