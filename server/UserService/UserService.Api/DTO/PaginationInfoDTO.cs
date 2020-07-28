using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class PaginationInfoDTO
    {
        public Guid AccountId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int SortActive { get; set; }

        public int SortDirection { get; set; }

        public int Filter { get; set; }

    }
}
