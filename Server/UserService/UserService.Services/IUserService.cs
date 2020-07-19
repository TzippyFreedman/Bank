using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserModel newUserModel);
    }
}
