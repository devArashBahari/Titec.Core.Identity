using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.EF.ModelBuilders.IdentityAggregate
{
    public class RolePermissionMap : IEntityTypeConfiguration<RolePermissionEntity>
    {
        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.ToTable("RolePermissions");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasKey(sc => new { sc.PermissionId, sc.RoleId });

            builder.HasOne(p => p.Permission)
               .WithMany(b => b.RolePermissions).HasForeignKey(m => m.PermissionId).IsRequired(false);

            builder.HasOne(p => p.Role)
              .WithMany(b => b.RolePermissions).HasForeignKey(m => m.RoleId).IsRequired(false);
        }
    }
}
