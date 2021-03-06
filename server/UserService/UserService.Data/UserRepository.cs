﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<bool> IsExistsAsync(string email)
        {
            bool isEmailExist = await _userDbContext.Users.AnyAsync(user => user.Email == email);
            return isEmailExist;
        }

        public async Task<UserModel> GetAsync(string email)
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


        public async Task<UserModel> GetByIdAsync(Guid userId)
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
        public async Task AddAsync(UserModel newUserModel)
        {
            User newUser = _mapper.Map<User>(newUserModel);
            newUser.Account = new Account();
            await _userDbContext.Users.AddAsync(newUser);
            await _userDbContext.SaveChangesAsync();
        }

        public async Task<UserModel> UpdateAsync(UserModel userModel)
        {
            User updatedUser = _mapper.Map<User>(userModel);
            User existingUser = await _userDbContext.Users
                .Where(user => user.UserId == updatedUser.UserId)
                .FirstOrDefaultAsync();
            if (existingUser == null)
            {
                throw new UserNotFoundException(updatedUser.UserId);
            }
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.Address = updatedUser.Address;
            await _userDbContext.SaveChangesAsync();
            return _mapper.Map<UserModel>(existingUser);



        }

        public async Task AddVerificationCodeAsync(EmailVerificationModel emailVerification)
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
                verificationToUpdate.ExpirationTime = DateTime.Now.AddMinutes(200);
            }
            await _userDbContext.SaveChangesAsync();
        }
        public async Task<EmailVerificationModel> GetVerificationCodeAsync(string email)
        {
            EmailVerification emailVerification = await _userDbContext.EmailVerifications
                .Where(verification => verification.Email == email)
                .FirstOrDefaultAsync();
            if (emailVerification == null)
            {
                throw new VerificationCodeNotFoundException(email);
            }
            return _mapper.Map<EmailVerificationModel>(emailVerification);
        }


    }

}
