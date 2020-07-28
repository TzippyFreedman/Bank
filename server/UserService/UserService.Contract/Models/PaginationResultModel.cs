using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract.Models
{
    public class PaginationResultModel
    {
      public  List<SucceededHistoryOperationModel> OperationsList {get; set;}
      public int OperationsTotal { get; set; }
    }
}
