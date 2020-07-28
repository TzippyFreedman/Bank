using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract.Models;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class OperationsHistoryService : IOperationsHistoryService
    {
        public async Task<PaginationResultModel<HistoryOperationModel>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
