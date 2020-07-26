using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransferService.Services.Models
{
    public class TransferModel
    {
        public Guid Id { get; set; }
        public TransferStatus Status { get; set; }
        public Guid FromAccount { get; set; }
        public Guid ToAccount { get; set; }
        public float Amount { get; set; }
    }
}
