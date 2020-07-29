using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public async Task AddSucceededOperation(SucceededHistoryOperationModel operationModel)
        {
            SucceededHistoryOperation operation = _mapper.Map<SucceededHistoryOperation>(operationModel);
            await _userDbContext.SucceededHistoryOperations.AddAsync(operation);
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

            IQueryable<SucceededHistoryOperation> operations = _userDbContext.SucceededHistoryOperations.Where(operation => operation.AccountId == paginationParams.AccountId);

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
            SortField sortField = paginationParams.SortField;

            List<SucceededHistoryOperation> operationList = await operations
                .OrderBy(sortField.ToString(), paginationParams.SortDirection
                .ToString() == "Asc" ? false : true)
                .Skip((paginationParams.PageIndex) * paginationParams.PageSize).Take(paginationParams.PageSize)
                .ToListAsync();

            response.OperationsList = _mapper.Map<List<SucceededHistoryOperationModel>>(operationList);
            return response;
        }

    }

    public static class OrderByHelper
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
