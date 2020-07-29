using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace UserService.Data.Entities
{
  public  class FailedHistoryOperation
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime OperationTime { get; set; }
        public bool IsCredit { get; set; }
        public int TransactionAmount { get; set; }
        public string  FailureReason { get; set; }
    }
}
