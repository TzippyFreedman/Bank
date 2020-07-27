using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserService.Data.Entities;

namespace UserService.Data
{
    //not used yet
    public interface IUserDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();

    }
}