using System.Threading.Tasks;

namespace UserService.Helpers.Interfaces
{
    public interface IEmailVerifier
    {
        string GenerateVerificationCode();
        Task SendVerificationEmailAsync(string emailAddress, string verificationCode);
    }
}