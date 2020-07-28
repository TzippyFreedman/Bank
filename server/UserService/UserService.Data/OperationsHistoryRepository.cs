using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Data.Entities;

namespace UserService.Data
{
    public class OperationsHistoryRepository : IOperationsHistoryRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public OperationsHistoryRepository(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }
        public async Task AddSuccessedOperation(HistoryOperationModel operationModel)
        {


            HistoryOperation operation = _mapper.Map<HistoryOperation>(operationModel);
            await _userDbContext.HistoryOperations.AddAsync(operation);

        }

        public async Task AddFailedOperation(FailedHistoryOperationModel operationModel)
        {

            
            FailedHistoryOperation operation = _mapper.Map<FailedHistoryOperation>(operationModel);
            await _userDbContext.FailedHistoryOperations.AddAsync(operation);

        }
    
    public async Task<PaginationResultModel> GetByFilter(PaginationParamsModel paginationParams)
        {
            PaginationResultModel response = new PaginationResultModel();

            string searchString = paginationParams.SearchString;

            IQueryable<HistoryOperation> operations = _userDbContext.HistoryOperations;

            if (!string.IsNullOrEmpty(searchString))
            {
                //If the search string changed during paging, the page is  reset to 0
                if (paginationParams.IsFilterChanged)
                {
                    paginationParams.PageIndex = 0;

                }

                operations = operations.Where(operation => operation.AccountId.ToString().Contains(searchString)
                                         || operation.Id.ToString().Contains(searchString)
                                         || operation.OperationTime.ToString().Contains(searchString)
                                         || operation.TransactionAmount.ToString().Contains(searchString));
            }

            response.OperationsTotal = await operations.CountAsync();

            List<HistoryOperation> operationList = await operations
                .OrderByDescending(operation => operation.OperationTime)
                .Skip((paginationParams.PageIndex) * paginationParams.PageSize).Take(paginationParams.PageSize)
                .ToListAsync();

            response.OperationList = _mapper.Map<List<HistoryOperationModel>>(operationList);

            return response;

        }
    }
}
