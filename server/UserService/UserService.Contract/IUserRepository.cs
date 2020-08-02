using System;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserAsync(string email);
        Task<bool> IsUserExistsAsync(string email);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task AddUserAsync(UserModel userModel);
        Task AddVerificationAsync(EmailVerificationModel emailVerification);
        Task<EmailVerificationModel> GetVerificationAsync(string email);
     
    }
}
