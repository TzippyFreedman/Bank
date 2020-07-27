namespace UserService.Services.Exceptions
{
    class IncorrectVerificationCodeException : BadRequestException
    {
        public IncorrectVerificationCodeException()
        {

        }
        public IncorrectVerificationCodeException(string verificationCode) : base($"Verification code:{verificationCode} is incorrect!!")
        {

        }
    }
}
