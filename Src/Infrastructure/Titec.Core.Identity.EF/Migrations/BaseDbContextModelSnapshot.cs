﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Titec.Core.Identity.EF.DataBaseContext;

#nullable disable

namespace Titec.Core.Identity.EF.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    partial class BaseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.AppEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastEditor")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Apps", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.PermissionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Creator")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastEditeDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastEditor")
                        .HasColumnType("int");

                    b.Property<byte>("MethodId")
                        .HasColumnType("tinyint");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSuperAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastEditor")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.RolePermissionEntity", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastEditor")
                        .HasColumnType("bigint");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserAppEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastEditor")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "AppId");

                    b.HasIndex("AppId");

                    b.ToTable("UserApps", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserCustomerEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CustumerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastEditor")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "CustumerId");

                    b.ToTable("UserCustomers", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastEditor")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MobileNo")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("OtpCode")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<DateTime?>("OtpCodeExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<short>("rowLevelAccess")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserRoleEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Creator")
                        .HasColumnType("bigint");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEditDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastEditor")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.PermissionEntity", b =>
                {
                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.AppEntity", "App")
                        .WithMany("Permissions")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.PermissionEntity", "Permission")
                        .WithMany("Permissions")
                        .HasForeignKey("ParentId");

                    b.Navigation("App");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.RoleEntity", b =>
                {
                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.AppEntity", "App")
                        .WithMany("Roles")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.RolePermissionEntity", b =>
                {
                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.PermissionEntity", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId");

                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.RoleEntity", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId");

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserAppEntity", b =>
                {
                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.AppEntity", "App")
                        .WithMany("UserApps")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.UserEntity", "User")
                        .WithMany("userApps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserCustomerEntity", b =>
                {
                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.UserEntity", "user")
                        .WithMany("userCustomers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserRoleEntity", b =>
                {
                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Titec.Core.Identity.Domain.AccountAggregate.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.AppEntity", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("Roles");

                    b.Navigation("UserApps");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.PermissionEntity", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.RoleEntity", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Titec.Core.Identity.Domain.AccountAggregate.UserEntity", b =>
                {
                    b.Navigation("UserRoles");

                    b.Navigation("userApps");

                    b.Navigation("userCustomers");
                });
#pragma warning restore 612, 618
        }
    }
}
