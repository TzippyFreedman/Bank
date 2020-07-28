using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
