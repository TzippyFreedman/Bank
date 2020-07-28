using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract
{
   public interface IOperationsHistoryRepository
    {
        Task<PaginationResultModel> GetByFilter(PaginationParamsModel paginationParams);
        Task AddFailedOperation(FailedHistoryOperationModel operation);
        Task AddSuccessedOperation(HistoryOperationModel operation);
    }
}
