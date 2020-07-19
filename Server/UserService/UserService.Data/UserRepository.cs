﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task AddAccountAsync(AccountModel newAccountModel)
        {
            Account newAccount = _mapper.Map<Account>(newAccountModel);

            _userDbContext.Accounts.Add(newAccount);

            await _userDbContext.SaveChangesAsync();

        }

        public async Task<UserModel> AddUserAsync(UserModel newUserModel)
        {
            User newUser = _mapper.Map<User>(newUserModel);

            _userDbContext.Users.Add(newUser);

            await _userDbContext.SaveChangesAsync();

            return _mapper.Map<UserModel>(newUser);
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
           bool isEmailExist = await _userDbContext.Users.AnyAsync(user => user.Email == email);
            return isEmailExist;
        }

        public async Task<UserModel> GetUser(string email, string password)
        {
            User user = await _userDbContext.Users
               .Where(user => user.Email == email && user.Password == password)
               .FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserModel>(user);

        }

    }
}
