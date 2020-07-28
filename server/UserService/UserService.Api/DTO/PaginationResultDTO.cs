using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class PaginationResultDTO
    {
      public  List<HistoryOperationDTO> OperationsList {get; set;}
        public int OperationsTotal { get; set; }
    }
}
