using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Api.Exceptions
{
    class VerificationCodeExpiredException : Exception
    {
        public VerificationCodeExpiredException()
        {

        }
        public VerificationCodeExpiredException(DateTime time) : base($"The expiration time was:{time} . please try again!")
        {

        }
    }
}
