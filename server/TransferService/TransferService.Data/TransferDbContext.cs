using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TransferService.Data.Entities;

namespace TransferService.Data
{
  public  class TransferDbContext : DbContext
    {

        public TransferDbContext(DbContextOptions options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transfer>()
                         .ToTable("Transfer");
            modelBuilder.Entity<Transfer>()
                        .Property(transfer => transfer.Status)
                        .HasConversion<string>();
            modelBuilder.Entity<Transfer>()
                        .Property(transfer => transfer.FromAccount)
                        .IsRequired();
            modelBuilder.Entity<Transfer>()
                       .Property(transfer => transfer.ToAccount)
                        .IsRequired();
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

        public DbSet<Transfer> Measures { get; set; }
    }
}

