using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class EmailVerificationDTO
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
