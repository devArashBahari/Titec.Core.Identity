using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.EF.ModelBuilders.IdentityAggregate
{
    public class RoleMap : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            // Columns
            builder.Property(e => e.Description).IsRequired(false).HasMaxLength(500);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Alias).IsRequired().HasMaxLength(200);

            builder.Property(e => e.IsAdmin).IsRequired();
            builder.Property(e => e.IsSuperAdmin).IsRequired();

            // Audit Columns
            builder.Property(e => e.Disabled).IsRequired();
            builder.Property(e => e.LastEditor).IsRequired(false);
            builder.Property(e => e.LastEditDate).IsRequired(false);
            builder.Property(e => e.Creator).IsRequired();
            builder.Property(e => e.CreateDate).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();

            // Relations

            builder.Property(e => e.AppId).IsRequired();
            builder.HasOne(p => p.App)
                .WithMany(b => b.Roles)
                .HasForeignKey(c => c.AppId);
        }
    }
}
