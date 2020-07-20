using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Api.Exceptions
{
    class WrongVerificationCodeException : Exception
    {
        public WrongVerificationCodeException()
        {

        }
        public WrongVerificationCodeException(string verificationCode) : base($"Verification code:{verificationCode} is wrong!!")
        {

        }
    }
}
