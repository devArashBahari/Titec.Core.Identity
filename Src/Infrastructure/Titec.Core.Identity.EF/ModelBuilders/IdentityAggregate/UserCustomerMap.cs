using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.EF.ModelBuilders.IdentityAggregate
{
    public class UserCustomerMap : IEntityTypeConfiguration<UserCustomerEntity>
    {
        public void Configure(EntityTypeBuilder<UserCustomerEntity> builder)
        {
            builder.ToTable("UserCustomers");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasKey(sc => new { sc.UserId, sc.CustumerId });

            builder.HasOne(p => p.user)
               .WithMany(b => b.userCustomers).HasForeignKey(m => m.UserId);
        }
    }
}
