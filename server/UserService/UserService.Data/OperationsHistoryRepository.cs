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
        public async Task AddSucceededOperation(SucceededOperationModel operationModel)
        {
            SucceededOperation operation = _mapper.Map<SucceededOperation>(operationModel);
            await _userDbContext.SucceededOperations.AddAsync(operation);
        }

        public async Task AddFailedOperation(FailedOperationModel operationModel)
        {
            FailedOperation operation = _mapper.Map<FailedOperation>(operationModel);
            await _userDbContext.FailedOperations.AddAsync(operation);
        }

        public async Task<PaginationResultModel> GetByFilter(PaginationParamsModel paginationParams)
        {
            PaginationResultModel response = new PaginationResultModel();

            string searchString = paginationParams.SearchString;

            IQueryable<SucceededOperation> operations = _userDbContext.SucceededOperations.Where(operation => operation.AccountId == paginationParams.AccountId);

            if (!string.IsNullOrEmpty(searchString))
            {
                operations = operations.Where(operation => operation.AccountId.ToString().Contains(searchString)
                                  || operation.Id.ToString().Contains(searchString)
                                  || operation.Balance.ToString().Contains(searchString)
                                  || operation.OperationTime.ToString().Contains(searchString)
                                  || operation.TransactionAmount.ToString().Contains(searchString)
                                 );
            }

            response.OperationsTotal = await operations.CountAsync();
            SortField sortField = paginationParams.SortField;

            List<SucceededOperation> operationList = await operations
                .OrderBy(sortField.ToString(), paginationParams.SortDirection
                .ToString() == "Asc" ? false : true)
                .Skip((paginationParams.PageIndex) * paginationParams.PageSize).Take(paginationParams.PageSize)
                .ToListAsync();

            response.OperationsList = _mapper.Map<List<SucceededOperationModel>>(operationList);
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
