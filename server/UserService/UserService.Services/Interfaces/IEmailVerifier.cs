using System.Threading.Tasks;

namespace UserService.Services.Interfaces
{
    public interface IEmailVerifier
    {
        string GenerateVerificationCode();
        Task SendVerificationEmail(string emailAddress, string verificationCode);
    }
}