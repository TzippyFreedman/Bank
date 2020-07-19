using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Data.Entities;
using UserService.Services;
using UserService.Services.Models;

namespace UserService.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public UserRepository(UserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<AccountModel> GetAccountInfoAsync(Guid accountId)
        {
           Account account=await  _userDbContext.Accounts.Where(account => account.Id == accountId)
                .FirstOrDefaultAsync();
            if(account!=null)
            {
                return _mapper.Map<AccountModel>(account);
            }
            else
            {
                return null;
            }
                 
                }

        public async  Task<UserModel> GetUser(string email, string password)
        {
             User user=  await   _userDbContext.Users
                .Where(user => user.Email == email && user.Password == password)
                .FirstOrDefaultAsync();
            if(user==null)
            {
                return null;
            }
            return _mapper.Map<UserModel>(user);
           
         }

        public async Task<AccountModel> GetUserAccountByUserId(Guid id)
        {

            Account userAccount = await _userDbContext.Accounts
                      .Where(file => file.UserId == id)
                      .FirstOrDefaultAsync();
            return _mapper.Map<AccountModel>(userAccount);

        }

    }
}
