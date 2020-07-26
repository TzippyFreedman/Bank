using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransferService.Api.DTO
{
    public class TransferDTO
    {
        public Guid FromAccount { get; set; }
        public Guid ToAccount { get; set; }
        public float Amount { get; set; }
    }
}
