﻿using System;
using System.Threading.Tasks;
using UserService.Services.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> LoginAsync(string email, string password);
        Task RegisterAsync(UserModel newUserModel, string password, string verificationCode);
        Task<AccountModel> GetAccountByIdAsync(Guid accountId);
        Task<UserModel> GetUserByIdAsync(Guid userId);
        Task VerifyEmailAsync(EmailVerificationModel emailVerification);

    }
}
