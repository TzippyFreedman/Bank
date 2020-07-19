using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserService.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        //change to hash 
        [Encrypted]
        public string Password { get; set; }

        public virtual Account Account { get; set; }
    }
}
