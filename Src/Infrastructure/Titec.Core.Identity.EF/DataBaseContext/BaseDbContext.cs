using Microsoft.EntityFrameworkCore;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.ModelBuilders.IdentityAggregate;
using Titec.Framework.Application.Identity;

namespace Titec.Core.Identity.EF.DataBaseContext
{
    public class BaseDbContext : DbContext
    {
        private readonly string _connectionString;
        //public BaseDbContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        public BaseDbContext(DbContextOptions<BaseDbContext> options, ITitecIdentity titecIdentity) : base(options)
        {
            
        }

        public DbSet<RoleEntity> roles { get; set; }
        public DbSet<UserEntity> users { get; set; }
        public DbSet<UserRoleEntity> userRoles { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RolePermissionEntity> RolePermissions { get; set; }
        public DbSet<AppEntity> appEntities { get; set; }
        public DbSet<UserAppEntity> userAppEntities { get; set; }
        public DbSet<UserCustomerEntity> userCustomerEntities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new PermissionMap());
            modelBuilder.ApplyConfiguration(new RolePermissionMap());
            modelBuilder.ApplyConfiguration(new AppMap());
            modelBuilder.ApplyConfiguration(new UserAppMap());
            modelBuilder.ApplyConfiguration(new UserCustomerMap());



            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}


    }
}
