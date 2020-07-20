using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Services.Exceptions
{
    class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException()
        {

        }
        public IncorrectPasswordException(string email) : base($"Login attempt with incorrect password for email:{email} is wrong!!")
        {

        }
    }
}
