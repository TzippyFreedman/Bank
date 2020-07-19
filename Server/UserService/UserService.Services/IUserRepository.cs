using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserAsync(string email, string password);
        Task<AccountModel> GetUserAccountByUserIdAsync(Guid id);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<UserModel> AddUserAsync(UserModel user);
        Task AddAccountAsync(AccountModel account);
        Task<AccountModel> GetAccountDetailsAsync(Guid accountId);
        Task<UserModel> GetUserByIdAsync(Guid id);

    }
}
