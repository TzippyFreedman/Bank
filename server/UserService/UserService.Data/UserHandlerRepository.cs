using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Data.Entities;
using UserService.NServiceBus.Services.Interfaces;

namespace UserService.Data
{
   public class UserHandlerRepository :IUserHandlerRepository
    { 
     private readonly UserDbContext _userDbContext;
    //private readonly IMapper _mapper;
    public UserHandlerRepository(UserDbContext userDbContext/*, IMapper mapper*/)
    {
        _userDbContext = userDbContext;
        //_mapper = mapper;

    }

    public async Task<bool> CheckBalanceAsync(Guid accountId, int amount)
    {
        Account user =await  _userDbContext.Accounts.Where(u => u.Id == accountId).FirstOrDefaultAsync();
        bool isBalanceOK = user.Balance >= amount ? true : false;
        return isBalanceOK;
    }

    public async Task<bool> CheckExistsAsync(Guid accountId)
    {
        return await _userDbContext.Accounts.AnyAsync(u => u.Id == accountId);
    }

    public async Task DrawAsync(Guid account, int amount)
    {
        Account userAccount = await _userDbContext.Accounts.Where(u => u.Id == account).FirstOrDefaultAsync();
        if (userAccount == null)
        {
            //throw new AccountDoesntExistException(account);
        }
        else
        {
                userAccount.Balance -= amount;
                //userAccount.UpdateDate = DateTime.Now;
            }
        //    await _userDbContext.SaveChangesAsync();

    }

    public async Task DepositAsync(Guid account, int amount)
    {
        Account userAccount = await _userDbContext.Accounts.Where(u => u.Id == account).FirstOrDefaultAsync();
        if (userAccount == null)
        {
            // throw new AccountDoesntExistException(account);
        }
        else
        {
                userAccount.Balance += amount;
                //userAccount.UpdateDate = DateTime.Now;

            }

        //    await _userDbContext.SaveChangesAsync();
    }


}
}
