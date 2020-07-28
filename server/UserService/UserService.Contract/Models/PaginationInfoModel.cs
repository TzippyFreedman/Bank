using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Contract.Models
{
    public class PaginationInfoModel
    {
        public Guid AccountId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int SortActive { get; set; }

        public int SortDirection { get; set; }

        public int Filter { get; set; }
    }
}
