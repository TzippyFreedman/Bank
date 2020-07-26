using System;

namespace UserService.Contract.Models
{
    public class EmailVerificationModel
    {
        public string Email { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Code { get; set; }
    }
}
