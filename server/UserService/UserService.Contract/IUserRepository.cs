using System;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract
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
        Task DrawAsync(Guid srcAccount, int amount);
        Task DepositAsync(Guid destAccount, int amount);
        Task<bool> IsExistsAsync(Guid accountId);
        Task<bool> IsBalanceOkAsync(Guid accountId, int amount);
    }
}
