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
        private readonly IEncryptionProvider _provider;
        private readonly AesKeyInfo _encryptionInfo;

        public UserDbContext(DbContextOptions options) : base(options)
        {
        
            //change to hash 
            _encryptionInfo = AesProvider.GenerateKey(AesKeySize.AES128Bits);
            _provider = new AesProvider(_encryptionInfo.Key, _encryptionInfo.IV);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                 .UseEncryption(_provider);

            modelBuilder.Entity<User>()
                .ToTable("User");
            modelBuilder.Entity<User>()
                        .Property(user => user.Email)
                        .IsRequired();
            modelBuilder.Entity<User>()
                       .Property(user => user.Id)
                       .HasDefaultValueSql("NEWID()");
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
                        .Property(user => user.Password)
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



        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
