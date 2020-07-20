using System;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<Guid> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(UserModel newUserModel, string password, string verificationCode);
        Task<AccountModel> GetAccountByIdAsync(Guid accountId);
        Task<UserModel> GetUserByIdAsync(Guid id);
        Task VerifyEmailAsync(EmailVerificationModel emailVerification);

    }
}
