using Microsoft.EntityFrameworkCore;
using UserService.Data.Entities;

namespace UserService.Data
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User");
            modelBuilder.Entity<User>()
                        .Property(user => user.Email)
                        .IsRequired();
            modelBuilder.Entity<User>()
                       .Property(user => user.Id)
                       .HasDefaultValueSql("NEWID()")
                       .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                        .HasIndex(user => user.Email)
                        .IsUnique();
            modelBuilder.Entity<User>()
                         .Property(user => user.FirstName)
                          .IsRequired();
            modelBuilder.Entity<User>()
                         .Property(user => user.LastName)
                          .IsRequired();
            modelBuilder.Entity<User>()
                        .Property(user => user.PasswordHash)
                        .IsRequired();
            modelBuilder.Entity<User>()
                        .Property(user => user.PasswordSalt)
                        .IsRequired();

            modelBuilder.Entity<Account>()
                        .ToTable("Account");
            modelBuilder.Entity<Account>()
                      .Property(account => account.Id)
                      .HasDefaultValueSql("NEWID()")
                      .ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>()
                         .Property(account => account.OpenDate)
                         .HasDefaultValueSql("getdate()")
                         .ValueGeneratedOnAdd();
            modelBuilder.Entity<Account>()
                        .Property(account => account.Balance)
                        .HasDefaultValue(100000);

            modelBuilder.Entity<EmailVerification>()
                        .ToTable("EmailVerification");
            modelBuilder.Entity<EmailVerification>()
                         .Property(verification => verification.Email)
                         .IsRequired();
            modelBuilder.Entity<EmailVerification>()
                        .Property(verification => verification.Code)
                        .IsRequired();
            modelBuilder.Entity<EmailVerification>()
                          .Property(verification => verification.ExpirationTime)
                          .IsRequired();
            modelBuilder.Entity<EmailVerification>()
                          .Property(verification => verification.ExpirationTime)
                          .HasDefaultValueSql("dateadd(minute,5,getdate())")
                          .ValueGeneratedOnAdd();

            modelBuilder.Entity<SucceededHistoryOperation>()
                .ToTable("SucceededHistoryOperation");
            modelBuilder.Entity<SucceededHistoryOperation>()
                        .Property(operation => operation.TransactionId)
                        .IsRequired();
            modelBuilder.Entity<SucceededHistoryOperation>()
                        .Property(operation => operation.AccountId)
                        .IsRequired();
            modelBuilder.Entity<SucceededHistoryOperation>()
                        .Property(operation => operation.Balance)
                        .IsRequired();
            modelBuilder.Entity<SucceededHistoryOperation>()
                        .Property(operation => operation.TransactionAmount)
                        .IsRequired();
            modelBuilder.Entity<SucceededHistoryOperation>()
                        .Property(operation => operation.IsCredit)
                        .IsRequired();
            modelBuilder.Entity<SucceededHistoryOperation>()
                        .Property(operation => operation.OperationTime)
                        .IsRequired();
            modelBuilder.Entity<SucceededHistoryOperation>()
                      .Property(operation => operation.Id)
                      .HasDefaultValueSql("NEWID()")
                      .ValueGeneratedOnAdd();
            
            

            modelBuilder.Entity<FailedHistoryOperation>()
                           .ToTable("FailedHistoryOperation");
            modelBuilder.Entity<FailedHistoryOperation>()
                       .Property(operation => operation.TransactionId)
                       .IsRequired();
            modelBuilder.Entity<FailedHistoryOperation>()
                        .Property(operation => operation.AccountId)
                        .IsRequired();
            modelBuilder.Entity<FailedHistoryOperation>()
                  .Property(operation => operation.TransactionAmount)
                  .IsRequired();
            modelBuilder.Entity<FailedHistoryOperation>()
            .Property(operation => operation.IsCredit)
            .IsRequired();
            modelBuilder.Entity<FailedHistoryOperation>()
                        .Property(operation => operation.OperationTime)
                        .IsRequired();
            modelBuilder.Entity<FailedHistoryOperation>()
                     .Property(operation => operation.Id)
                     .HasDefaultValueSql("NEWID()")
                     .ValueGeneratedOnAdd();
         
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        public DbSet<SucceededHistoryOperation> SucceededHistoryOperations { get; set; }

        public DbSet<FailedHistoryOperation> FailedHistoryOperations { get; set; }



    }
}
