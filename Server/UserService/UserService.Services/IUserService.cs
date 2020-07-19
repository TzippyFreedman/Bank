using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<Guid> LoginAsync(string email,string password);
    }
}
