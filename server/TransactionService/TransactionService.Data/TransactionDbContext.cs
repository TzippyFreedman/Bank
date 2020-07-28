using Microsoft.EntityFrameworkCore;
using TransactionService.Data.Entities;

namespace TransactionService.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                         .ToTable("Transaction");
            modelBuilder.Entity<Transaction>()
                        .Property(transaction => transaction.Status)
                        .HasConversion<string>();
            modelBuilder.Entity<Transaction>()
                        .Property(transaction => transaction.SrcAccountId)
                        .IsRequired();
            modelBuilder.Entity<Transaction>()
                       .Property(transaction => transaction.DestAccountId)
                        .IsRequired();
            modelBuilder.Entity<Transaction>()
                        .Property(transaction => transaction.Date)
                        .HasDefaultValueSql("getdate()")
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Transaction>()
                         .Property(transaction => transaction.Status)
                        .IsRequired();
            modelBuilder.Entity<Transaction>()
                       .Property(transaction => transaction.Amount)
                        .IsRequired();
            modelBuilder.Entity<Transaction>()
                       .Property(transaction => transaction.Id)
                       .HasDefaultValueSql("NEWID()")
                        .ValueGeneratedOnAdd();
        }
        public DbSet<Transaction> Transactions { get; set; }
    }
}

