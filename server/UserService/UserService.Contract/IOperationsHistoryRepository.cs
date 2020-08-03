using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract
{
   public interface IOperationsHistoryRepository
    {
        Task<PaginationResultModel> GetByFilter(PaginationParamsModel paginationParams);
        Task AddFailedOperation(FailedOperationModel operation);
        Task AddSucceededOperation(SucceededOperationModel operation);


    }
}
