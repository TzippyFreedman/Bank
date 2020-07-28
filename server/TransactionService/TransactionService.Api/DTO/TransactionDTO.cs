using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TransactionService.Api.DTO
{
    public class TransactionDTO
    {
        [Required]
        public Guid SrcAccountId { get; set; }
        [Required]
        public Guid DestAccountId { get; set; }
        [Required]
        [Range(1,1000000)]
        [RegularExpression(@"^\d+.?\d{0,2}$",
        ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        public float Amount { get; set; }
    }
}
