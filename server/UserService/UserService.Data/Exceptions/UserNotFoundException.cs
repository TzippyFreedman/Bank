using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Data.Exceptions
{
    public class UserNotFoundException : DataNotFoundException
    {
        public UserNotFoundException()
        {

        }
        public UserNotFoundException(Guid userId) : base($"User with id:{userId} was not found.")
        {

        }
        public UserNotFoundException(string email) : base($"User with email:{email} was not found.")
        {

        }
    }
}
