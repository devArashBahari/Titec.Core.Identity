using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.EF.ModelBuilders.IdentityAggregate
{
    public class PermissionMap : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            // Table
            builder.ToTable("Permissions");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
            // Columns
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);

            // Audit Columns

            builder.Property(e => e.Path).IsRequired();
            builder.Property(e => e.MethodId).IsRequired();
            //builder.Property(e => e.MethodTitle).IsRequired();
            builder.Property(e => e.isDeleted).IsRequired();
            builder.Property(e => e.Alias).IsRequired();

            builder.Property(e => e.CreateDate).IsRequired();
            builder.Property(e => e.Creator).IsRequired();
            builder.Property(e => e.LastEditeDate).IsRequired(false);
            builder.Property(e => e.LastEditor).IsRequired(false);
            // Relations

            builder.HasOne(p => p.Permission)
                .WithMany(b => b.Permissions)
                .HasForeignKey(c => c.ParentId).IsRequired(false);

            builder.Property(e => e.AppId).IsRequired();
            builder.HasOne(p => p.App)
                .WithMany(b => b.Permissions)
                .HasForeignKey(c => c.AppId);

        }
    }
}
