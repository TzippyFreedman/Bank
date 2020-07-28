using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Contract.Models
{
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
        public bool IsFilterChanged { get; set; }

    }
}
