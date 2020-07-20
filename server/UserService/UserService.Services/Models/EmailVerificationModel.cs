using System;

namespace UserService.Services.Models
{
   public class EmailVerificationModel
    {
        public string Email { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Code { get; set; }
    }
}
