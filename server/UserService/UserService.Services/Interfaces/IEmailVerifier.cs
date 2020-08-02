namespace UserService.Services.Interfaces
{
    public interface IEmailVerifier
    {
        string GenerateVerificationCode();
        void SendVerificationEmail(string emailAddress, string verificationCode);
    }
}