using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Services.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OpenDate { get; set; }
        public float Balance { get; set; }
    }
}
