﻿using System;

namespace UserService.Contract.Models
{
    public enum SortField
    {
        OperationTime,
        TransactionAmount,
        Balance,
        IsCredit,
        TransactionId
    }
    public enum SortDirection
    {
        Asc,
        Desc
    }
    public class PaginationParamsModel
    {
        private const int maxPageSize = 50;
        public Guid AccountId { get; set; }
        public int PageIndex { get; set; } = 0;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;

            }
        }
        public string SearchString { get; set; }
        public SortField SortField { get; set; }
        public SortDirection SortDirection { get; set; }

    }
}
