using System;

namespace UserService.Services.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException()
        {

        }
        public IncorrectPasswordException(string email) : base($"Login attempt with incorrect password for email:{email}.")
        {

        }
    }
}
