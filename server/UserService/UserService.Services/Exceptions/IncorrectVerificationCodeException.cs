namespace UserService.Services.Exceptions
{
   public class IncorrectVerificationCodeException : BadRequestException
    {
        public IncorrectVerificationCodeException()
        {

        }
        public IncorrectVerificationCodeException(string verificationCode) : base($"Verification code:{verificationCode} is incorrect!!")
        {

        }
    }
}
