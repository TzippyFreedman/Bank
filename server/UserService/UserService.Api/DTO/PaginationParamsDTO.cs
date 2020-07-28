using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class PaginationParamsDTO
    {
        [Required]
        public Guid AccountId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }       
        public string SearchString { get; set; }
        [Required]
        public bool IsFilterChanged { get; set; }

    }
}
