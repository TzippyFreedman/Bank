using System;

namespace UserService.Data.Entities
{
    //check if entities is necessary 

    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OpenDate { get; set; }
        public int Balance { get; set; }
        public virtual User User { get; set; }

    }
}
