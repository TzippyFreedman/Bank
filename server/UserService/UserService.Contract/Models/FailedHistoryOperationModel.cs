using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Contract.Models
{
   public class FailedHistoryOperationModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime OperationTime { get; set; }
        public bool IsCredit { get; set; }
        public int TransactionAmount { get; set; }
    }
}
