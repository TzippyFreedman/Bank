using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

//check if entities is necessary 
namespace UserService.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

<<<<<<< HEAD
        //change to hash 
        /*  [Encrypted]
          public string Password { get; set; }*/
        [Required]
        public string PasswordHash { get; set; }
        [Required]

=======
        public string PasswordHash { get; set; }
>>>>>>> 68337d1ea66a061587107715c577d1e7f5747102
        public string PasswordSalt { get; set; }

        public virtual Account Account { get; set; }
    }
}
