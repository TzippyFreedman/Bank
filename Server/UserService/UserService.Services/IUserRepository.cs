using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public interface IUserRepository
    {
        Task<UserModel> GetUser(string email, string password);
         Task<AccountModel> GetUserAccountByUserId(Guid id)

    }
}
