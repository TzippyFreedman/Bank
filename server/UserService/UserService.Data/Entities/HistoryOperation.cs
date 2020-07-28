using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace UserService.Data.Entities
{
    public class HistoryOperation
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid TransactionId { get; set; }
        public bool IsCredit { get; set; }
        public int TransactionAmount { get; set; }
        public int Balance { get; set; }
        public DateTime OperationTime { get; set; }

    }
}
