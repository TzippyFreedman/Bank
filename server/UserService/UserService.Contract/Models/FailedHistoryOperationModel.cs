using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Contract.Models
{
   public class FailedHistoryOperationModel : HistoryOperationModel
    {
        public string FailureReason { get; set; }
    }
}
