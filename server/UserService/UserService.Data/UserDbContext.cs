using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using System;
using System.Collections.Generic;
using System.Text;
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
            /*            modelBuilder.Entity<Account>()
                                    .Property(account => account.UserId)
                                    .IsRequired();*/
            modelBuilder.Entity<Account>()
                        .Property(account => account.Balance)
                        .HasDefaultValue(1000);

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

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }


    }
}
