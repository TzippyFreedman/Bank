using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Contract.Models
{
    class SuccessedHistoryOperationModel : HistoryOperationModel
    {
        public int Balance { get; set; }
    }
}
