using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Entities
{
   public class EmailVerification
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
