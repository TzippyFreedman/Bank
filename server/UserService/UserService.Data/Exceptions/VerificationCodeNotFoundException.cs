namespace UserService.Data.Exceptions
{
    public class VerificationCodeNotFoundException : DataNotFoundException
    {
        public VerificationCodeNotFoundException()
        {

        }
        public VerificationCodeNotFoundException(string email) : base($"Verification for email :{email} was not found.")
        {

        }
    }
}
