using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public interface IUserRepository
    {
        Task<bool> CheckEmailExistsAsync(string email);
        Task<UserModel> AddUserAsync(UserModel user);
        Task AddAccountAsync(AccountModel account);
    }
}
