using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.EF.ModelBuilders.IdentityAggregate
{
    public class UserAppMap : IEntityTypeConfiguration<UserAppEntity>
    {
        public void Configure(EntityTypeBuilder<UserAppEntity> builder)
        {
            builder.ToTable("UserApps");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasKey(sc => new { sc.UserId, sc.AppId });

            builder.HasOne(p => p.User)
               .WithMany(b => b.userApps).HasForeignKey(m => m.UserId);

            builder.HasOne(p => p.App)
              .WithMany(b => b.UserApps).HasForeignKey(m => m.AppId);
        }
    }
}
