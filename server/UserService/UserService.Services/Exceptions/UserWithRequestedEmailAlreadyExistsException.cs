using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Services.Exceptions
{
    class UserWithRequestedEmailAlreadyExistsException : Exception
    {
        public UserWithRequestedEmailAlreadyExistsException()
        {

        }
        public UserWithRequestedEmailAlreadyExistsException(string email) : base($"Email:{email} already exists!")
        {

        }
    }
}
