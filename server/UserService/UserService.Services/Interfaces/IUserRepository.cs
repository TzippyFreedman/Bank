using System;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserAsync(string email);
        Task<AccountModel> GetAccountByUserIdAsync(Guid userId);
        Task<bool> IsUserExistsAsync(string email);   
        Task<AccountModel> GetAccountByIdAsync(Guid accountId);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task AddUserAsync(UserModel userModel);
        Task AddVerificationAsync(EmailVerificationModel emailVerification);
        Task<EmailVerificationModel> GetVerificationAsync( string email);
    }
}
