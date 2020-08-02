using System.Threading.Tasks;

namespace UserService.Services.Interfaces
{
    public interface IEmailVerifier
    {
        string GenerateVerificationCode();
        Task SendVerificationEmailAsync(string emailAddress, string verificationCode);
    }
}