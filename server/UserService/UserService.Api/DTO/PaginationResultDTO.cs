using System.Collections.Generic;


namespace UserService.Api.DTO
{
    public class PaginationResultDTO
    {
      public  List<SucceededOperationDTO> OperationsList {get; set;}
      public int OperationsTotal { get; set; }
    }
}
