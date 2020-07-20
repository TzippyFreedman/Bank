
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.Entities;
using UserService.Services.Models;
using UserService.Services;
using UserService.Data.Exceptions;

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

        public async Task<bool> CheckUserExistsAsync(string email)
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
                throw new UserNotFoundException();
            }
            return _mapper.Map<UserModel>(user);
        }

        public async Task<AccountModel> GetAccountByUserIdAsync(Guid accountId)
        {
            Account account = await _userDbContext.Accounts
                      .Where(file => file.UserId == accountId)
                      .FirstOrDefaultAsync();
            if (account == null)
            {

                throw new AccountNotFoundException(accountId);
            }
            return _mapper.Map<AccountModel>(account);
        }

        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            Account account = await _userDbContext.Accounts
                  .Where(account => account.Id == accountId)
                  .FirstOrDefaultAsync();
            if (account == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            return _mapper.Map<AccountModel>(account);
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            User user = await _userDbContext.Users
                  .Where(user => user.Id == userId)?
                  .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }
            return _mapper.Map<UserModel>(user);
        }
        public async Task AddUserAsync(UserModel newUserModel)
        {
            User newUser = _mapper.Map<User>(newUserModel);
            newUser.Account = new Account();
            _userDbContext.Users.Add(newUser);
            await _userDbContext.SaveChangesAsync();
        }

        public async Task AddVerificationAsync(EmailVerificationModel emailVerification)
        {
            Entities.EmailVerification verification = _mapper.Map<Entities.EmailVerification>(emailVerification);
            bool isEmailExist = await _userDbContext.EmailVerifications.AnyAsync(v => v.Email == verification.Email);
            if (isEmailExist == false)
            {
                _userDbContext.EmailVerifications.Add(verification);
            }
            else
            {
                Entities.EmailVerification verificationToUpdate = await _userDbContext.EmailVerifications
                    .Where(verification => verification.Email == emailVerification.Email)
                    .FirstOrDefaultAsync();
                verificationToUpdate.Code = verification.Code;
                verificationToUpdate.ExpirationTime = DateTime.Now.AddMinutes(5);
            }
            await _userDbContext.SaveChangesAsync();
        }
        public async Task<EmailVerificationModel> GetVerificationAsync(string email)
        {
            Entities.EmailVerification emailVerification = await _userDbContext.EmailVerifications
                .Where(verification => verification.Email == email)
                .FirstOrDefaultAsync();
            if (emailVerification == null)
            {
                throw new VerificationNotFoundException(email);
            }
            return _mapper.Map<EmailVerificationModel>(emailVerification);
        }

    }
}
