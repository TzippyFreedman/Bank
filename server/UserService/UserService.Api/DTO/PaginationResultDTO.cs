using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class PaginationResultDTO
    {
      public  List<HistoryOperationDTO> operations {get; set;}
        public int Total { get; set; }
    }
}
