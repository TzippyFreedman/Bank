namespace UserService.Services.Interfaces
{
    public interface IVerifyEmail
    {
        string GenerateVerificationCode();
        void SendVerificationEmail(string emailAddress, string verificationCode);
    }
}