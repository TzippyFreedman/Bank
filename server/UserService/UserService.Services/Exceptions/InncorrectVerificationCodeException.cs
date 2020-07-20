using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Api.Exceptions
{
    class InncorrectVerificationCodeException : Exception
    {
        public InncorrectVerificationCodeException()
        {

        }
        public InncorrectVerificationCodeException(string verificationCode) : base($"Verification code:{verificationCode} is wrong!!")
        {

        }
    }
}
