using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Api.Exceptions
{
    class ExpirationTimeOverException : Exception
    {
        public ExpirationTimeOverException()
        {

        }
        public ExpirationTimeOverException(DateTime time) : base($"The expiration time was:{time} . please try again!")
        {

        }
    }
}
