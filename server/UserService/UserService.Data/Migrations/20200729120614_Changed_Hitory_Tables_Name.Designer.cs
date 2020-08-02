﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Data;

namespace UserService.Data.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20200729120614_Changed_Hitory_Tables_Name")]
    partial class Changed_Hitory_Tables_Name
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserService.Data.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(100000);

                    b.Property<DateTime>("OpenDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("UserService.Data.Entities.EmailVerification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("dateadd(minute,5,getdate())");

                    b.HasKey("Id");

                    b.ToTable("EmailVerification");
                });

            modelBuilder.Entity("UserService.Data.Entities.FailedOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FailureReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCredit")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionAmount")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("FailedOperation");
                });

            modelBuilder.Entity("UserService.Data.Entities.SucceededOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<bool>("IsCredit")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionAmount")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("SucceededOperation");
                });

            modelBuilder.Entity("UserService.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("UserService.Data.Entities.Account", b =>
                {
                    b.HasOne("UserService.Data.Entities.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("UserService.Data.Entities.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
