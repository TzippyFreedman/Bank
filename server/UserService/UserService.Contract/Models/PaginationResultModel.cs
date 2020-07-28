using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract.Models
{
    public class PaginationResultModel<T>
    {
      public  List<HistoryOperationModel> operations {get; set;}
        public int Total { get; set; }
    }
}
