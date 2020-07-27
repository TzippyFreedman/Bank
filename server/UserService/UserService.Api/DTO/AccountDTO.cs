using System;

namespace UserService.Api.DTO
{
    public class AccountDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Balance { get; set; }
        public string Email { get; set; }
        public DateTime OpenDate { get; set; }

    }
}
