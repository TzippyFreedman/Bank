using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Contract.Models
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
