using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TransferService.Api.DTO
{
    public class TransferDTO
    {
        [Required]
        public Guid FromAccount { get; set; }
        [Required]
        public Guid ToAccount { get; set; }
        [Required]
        [Range(1,1000000)]
        public float Amount { get; set; }
    }
}
