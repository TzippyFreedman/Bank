using System;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Contract
{
    public interface IUserRepository
    {
        Task<UserModel> GetAsync(string email);
        Task<bool> IsExistsAsync(string email);
        Task<UserModel> GetByIdAsync(Guid userId);
        Task AddAsync(UserModel userModel);
        Task<UserModel> UpdateAsync(UserModel userModel);

        Task AddVerificationCodeAsync(EmailVerificationModel emailVerification);
        Task<EmailVerificationModel> GetVerificationCodeAsync(string email);
     
    }
}
