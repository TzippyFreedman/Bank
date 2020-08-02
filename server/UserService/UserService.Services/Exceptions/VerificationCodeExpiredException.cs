using System;

namespace UserService.Services.Exceptions
{
    class VerificationCodeExpiredException : BadRequestException
    {
        public VerificationCodeExpiredException()
        {

        }
        public VerificationCodeExpiredException(DateTime time) : base($"Verification code has expired. The expiration time was:{time}.")
        {

        }
    }
}
