using System;

namespace UserService.Contract.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public String UserId { get; set; }

        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
