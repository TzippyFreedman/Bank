using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Contract.Models;

namespace UserService.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountModel> GetAccountByIdAsync(Guid accountId);
    }
}
