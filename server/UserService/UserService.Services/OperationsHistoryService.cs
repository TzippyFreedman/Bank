using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class OperationsHistoryService : IOperationsHistoryService
    {
        private readonly IOperationsHistoryRepository _operationsHistoryRepository;

        public OperationsHistoryService(IOperationsHistoryRepository operationsHistoryRepository)
        {
            _operationsHistoryRepository = operationsHistoryRepository;
        }
        public async Task<PaginationResultModel> GetByFilter(PaginationParamsModel paginationParams)
        {
           return await _operationsHistoryRepository.GetByFilter(paginationParams);
        }
    }
}
