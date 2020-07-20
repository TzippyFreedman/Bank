
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
      
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            bool isEmailExist = await _userDbContext.Users.AnyAsync(user => user.Email == email);
            return isEmailExist;
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            User user = await _userDbContext.Users
               .Where(user => user.Email == email)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserModel>(user);

        }

        public async Task<AccountModel> GetAccountByUserIdAsync(Guid id)
        {
            Account userAccount = await _userDbContext.Accounts
                      .Where(file => file.UserId == id)
                      .FirstOrDefaultAsync();
            return _mapper.Map<AccountModel>(userAccount);
        }

        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            Account account = await _userDbContext.Accounts
                  .Where(account => account.Id == accountId)
                  .FirstOrDefaultAsync();

            if (account == null)
            {
                return null;
            }
            return _mapper.Map<AccountModel>(account);
        }

        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            User user = await _userDbContext.Users
                  .Where(user => user.Id == id)?
                  .FirstOrDefaultAsync();

            return _mapper.Map<UserModel>(user);
        }
        public async Task AddUserAsync(UserModel newUserModel)
        {
            User newUser = _mapper.Map<User>(newUserModel);


            newUser.Account = new Account();
            _userDbContext.Users.Add(newUser);

          await  _userDbContext.SaveChangesAsync();
        }

        public async Task AddVerificationAsync(EmailVerificationModel emailVerification)
        {
            EmailVerification verification= _mapper.Map<EmailVerification>(emailVerification);
            _userDbContext.EmailVerifications.Add(verification);
            await _userDbContext.SaveChangesAsync();
        }

    }
}
