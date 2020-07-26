using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Data.Entities;

namespace UserService.Data
{
   public class UserHandlerRepository /*: IUserHandlerRepository*/
    { 
     private readonly UserDbContext _userDbContext;
    private readonly IMapper _mapper;
    public UserHandlerRepository(UserDbContext userDbContext, IMapper mapper)
    {
        _userDbContext = userDbContext;
        _mapper = mapper;

    }

    public bool CheckBalance(Guid accountId, float amount)
    {
        Account user = _userDbContext.Accounts.Where(u => u.Id == accountId).FirstOrDefault();
        bool isBalanceOK = user.Balance >= amount ? true : false;
        return isBalanceOK;
    }

    public async Task<bool> CheckExists(Guid accountId)
    {
        return await _userDbContext.Accounts.AnyAsync(u => u.Id == accountId);
    }

    public async Task Pull(Guid account, float amount)
    {
        //include??
        //User user=await _userDbContext.Users.Where(u => u.UserFile.Id == srcAccount).FirstOrDefaultAsync();
        Account userAccount = await _userDbContext.Accounts.Where(u => u.Id == account).FirstOrDefaultAsync();
        if (userAccount == null)
        {
            //throw new AccountDoesntExistException(account);
        }
        else
        {
            //userAccount.Balance -= amount;
            //userAccount.UpdateDate = DateTime.Now;
        }
        //    await _userDbContext.SaveChangesAsync();

    }

    public async Task Push(Guid account, float amount)
    {
        Account userAccount = await _userDbContext.Accounts.Where(u => u.Id == account).FirstOrDefaultAsync();
        if (userAccount == null)
        {
            // throw new AccountDoesntExistException(account);
        }
        else
        {
            //userAccount.Balance += amount;
            //userAccount.UpdateDate = DateTime.Now;

        }

        //    await _userDbContext.SaveChangesAsync();
    }


}
}
