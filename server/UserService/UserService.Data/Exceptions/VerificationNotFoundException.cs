namespace UserService.Data.Exceptions
{
    public class VerificationNotFoundException : DataNotFoundException
    {
        public VerificationNotFoundException()
        {

        }
        public VerificationNotFoundException(string email) : base($"Verification for email :{email} was not found.")
        {

        }
    }
}
