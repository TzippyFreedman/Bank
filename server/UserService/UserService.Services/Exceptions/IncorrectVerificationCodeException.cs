using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Api.Exceptions
{
    class IncorrectVerificationCodeException : Exception
    {
        public IncorrectVerificationCodeException()
        {

        }
        public IncorrectVerificationCodeException(string verificationCode) : base($"Verification code:{verificationCode} is incorrect!!")
        {

        }
    }
}
