using System.Collections.Generic;

namespace UserService.Contract.Models
{
    public class PaginationResultModel
    {
      public  List<SucceededOperationModel> OperationsList {get; set;}
      public int OperationsTotal { get; set; }
    }
}
