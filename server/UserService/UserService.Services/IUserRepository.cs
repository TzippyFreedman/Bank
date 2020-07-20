﻿using System;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserAsync(string email);
        Task<AccountModel> GetAccountByUserIdAsync(Guid id);
        Task<bool> CheckUserExistsAsync(string email);   
        Task<AccountModel> GetAccountByIdAsync(Guid accountId);
        Task<UserModel> GetUserByIdAsync(Guid id);
        Task AddUserAsync(UserModel userModel);
        Task AddVerificationAsync(EmailVerificationModel emailVerification);
        Task<EmailVerificationModel> GetVerificationAsync( string email);
    }
}