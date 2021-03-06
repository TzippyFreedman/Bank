﻿using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Services.Interfaces
{
    public interface IOperationsHistoryService
    {
        Task<PaginationResultModel> GetByFilter(PaginationParamsModel paginationParams);
    }
}
