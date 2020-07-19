using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Entities
{
    //check if entities is necessary 

    public class Account
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime OpenDate { get; set; }
   

        public float Balance { get; set; }
        public virtual User user { get; set; }

    }
}
