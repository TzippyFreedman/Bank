using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Services.Exceptions
{
    class UserWithRequestedEmailExistsException : Exception
    {
        public UserWithRequestedEmailExistsException()
        {

        }
        public UserWithRequestedEmailExistsException(string email) : base($"Email:{email} already exists! please try again!")
        {

        }
    }
}
