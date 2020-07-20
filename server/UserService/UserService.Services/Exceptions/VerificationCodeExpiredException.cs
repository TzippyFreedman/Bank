using System;

namespace UserService.Services.Exceptions
{
    class VerificationCodeExpiredException : BadRequestException
    {
        public VerificationCodeExpiredException()
        {

        }
        public VerificationCodeExpiredException(DateTime time) : base($"The expiration time was:{time}")
        {

        }
    }
}
