﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Services.Models
{
   public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //public virtual Account Account { get; set; }
    }
}
