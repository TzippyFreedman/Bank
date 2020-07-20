using System;

namespace UserService.Services.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OpenDate { get; set; }
        public int Balance { get; set; }
    }
}
