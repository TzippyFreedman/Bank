using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Services.Exceptions
{
    class EmailExistsException : Exception
    {
        public EmailExistsException()
        {

        }
        public EmailExistsException(string email) : base($"Email:{email} already exists! please try again!")
        {

        }
    }
}
