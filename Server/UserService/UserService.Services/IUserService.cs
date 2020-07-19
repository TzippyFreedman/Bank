using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<Guid> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(UserModel newUserModel);
      Task<AccountModel> GetAccountDetailsAsync(Guid accountId);
    }
}
