using Microsoft.EntityFrameworkCore;
using TransferService.Data.Entities;

namespace TransferService.Data
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>()
                         .ToTable("Transfer");
            modelBuilder.Entity<Transfer>()
                        .Property(transfer => transfer.Status)
                        .HasConversion<string>();
            modelBuilder.Entity<Transfer>()
                        .Property(transfer => transfer.SrcAccountId)
                        .IsRequired();
            modelBuilder.Entity<Transfer>()
                       .Property(transfer => transfer.DestAccountId)
                        .IsRequired();
            modelBuilder.Entity<Transfer>()
                        .Property(transfer => transfer.Date)
                        .HasDefaultValueSql("getdate()")
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Transfer>()
                         .Property(transfer => transfer.Status)
                        .IsRequired();
            modelBuilder.Entity<Transfer>()
                       .Property(transfer => transfer.Amount)
                        .IsRequired();
            modelBuilder.Entity<Transfer>()
                       .Property(transfer => transfer.Id)
                       .HasDefaultValueSql("NEWID()")
                        .ValueGeneratedOnAdd();
        }
        public DbSet<Transfer> Transfers { get; set; }
    }
}

