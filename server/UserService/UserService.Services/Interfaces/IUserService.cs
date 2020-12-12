using System;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(string email, string password);
        Task RegisterAsync(UserModel newUserModel, string password, string verificationCode);
        Task<UserModel> Update(UserModel newUserModel);

        Task<UserModel> GetByIdAsync(Guid userId);
        Task AddVerificationAsync(EmailVerificationModel emailVerification);
    }
}
