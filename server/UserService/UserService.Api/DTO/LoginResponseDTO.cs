﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Api.DTO
{
    public class LoginResponseDTO
    {
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
