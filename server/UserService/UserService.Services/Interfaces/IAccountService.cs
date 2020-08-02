using System;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountModel> GetByIdAsync(Guid accountId);
    }
}
