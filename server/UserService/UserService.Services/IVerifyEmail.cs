namespace UserService.Services
{
    public interface IVerifyEmail
    {
        string GenerateVerificationCode();
        void SendVerificationEmail(string emailAddress, string verificationCode);
    }
}