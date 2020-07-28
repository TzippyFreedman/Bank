using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Data.Entities;
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

        public async Task<bool> IsUserExistsAsync(string email)
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
                throw new UserNotFoundException(email);
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
            await _userDbContext.Users.AddAsync(newUser);
            await _userDbContext.SaveChangesAsync();
        }

        public async Task AddVerificationAsync(EmailVerificationModel emailVerification)
        {
            EmailVerification verification = _mapper.Map<EmailVerification>(emailVerification);
            bool isEmailExist = await _userDbContext.EmailVerifications
                .AnyAsync(v => v.Email == verification.Email);
            if (isEmailExist == false)
            {
                await _userDbContext.EmailVerifications.AddAsync(verification);
            }
            else
            {
                EmailVerification verificationToUpdate = await _userDbContext.EmailVerifications
                    .Where(verification => verification.Email == emailVerification.Email)
                    .FirstOrDefaultAsync();
                verificationToUpdate.Code = verification.Code;
                verificationToUpdate.ExpirationTime = DateTime.Now.AddMinutes(5);
            }
            await _userDbContext.SaveChangesAsync();
        }
        public async Task<EmailVerificationModel> GetVerificationAsync(string email)
        {
            EmailVerification emailVerification = await _userDbContext.EmailVerifications
                .Where(verification => verification.Email == email)
                .FirstOrDefaultAsync();
            if (emailVerification == null)
            {
                throw new VerificationNotFoundException(email);
            }
            return _mapper.Map<EmailVerificationModel>(emailVerification);
        }
        public async Task<bool> IsBalanceOkAsync(Guid accountId, int amount)
        {
            Account user = await _userDbContext.Accounts
                .Where(u => u.Id == accountId)
                .FirstOrDefaultAsync();
            bool isBalanceOK = user.Balance >= amount ? true : false;
            return isBalanceOK;
        }

        public async Task<bool> IsExistsAsync(Guid accountId)
        {
            return await _userDbContext.Accounts.AnyAsync(u => u.Id == accountId);
        }

        public async Task<int> DrawAsync(Guid accountId, int amount)
        {
            Account userAccount = await _userDbContext.Accounts
                .Where(u => u.Id == accountId)
                .FirstOrDefaultAsync();
            if (userAccount == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            if (userAccount.Balance < amount)
            {
                throw new InsufficientBalanceForTransactionException(accountId, amount);
            }
            userAccount.Balance -= amount;
            return userAccount.Balance;

        }

        public async Task<int> DepositAsync(Guid accountId, int amount)
        {
            Account userAccount = await _userDbContext.Accounts
                .Where(u => u.Id == accountId)
                .FirstOrDefaultAsync();
            if (userAccount == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            userAccount.Balance += amount;
            return userAccount.Balance;
        }

      
    }

}
