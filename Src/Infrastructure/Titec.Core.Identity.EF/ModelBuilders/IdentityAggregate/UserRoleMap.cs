using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Domain.AccountAggregate;

namespace Titec.Core.Identity.EF.ModelBuilders.IdentityAggregate
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.HasKey(sc => new { sc.UserId, sc.RoleId });

            builder.HasOne(p => p.User)
               .WithMany(b => b.UserRoles).HasForeignKey(m => m.UserId);

            builder.HasOne(p => p.Role)
              .WithMany(b => b.UserRoles).HasForeignKey(m => m.RoleId);
        }
    }
}
