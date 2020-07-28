using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class HistoryOperationDTO
    {
        public Guid TransactionId { get; set; }
        public DateTime OperationTime { get; set; }
        public float Amount { get; set; }
        public float Balance { get; set; }
        public bool IsDebit { get; set; }
        public Guid AccountId { get; set; }

    }
}
