using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UserService.Data.Entities;

namespace UserService.Data
{
    public interface IUserDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();

    }
}