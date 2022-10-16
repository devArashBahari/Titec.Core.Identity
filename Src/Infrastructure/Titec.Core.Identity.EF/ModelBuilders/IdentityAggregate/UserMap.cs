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
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Table
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
            // Columns
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.Property(e => e.OtpCode).IsRequired(false).HasMaxLength(6);
            builder.Property(e => e.OtpCodeExpiry).IsRequired(false);
            builder.Property(e => e.UserName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.MobileNo).IsRequired().HasMaxLength(11);
            builder.Property(e => e.rowLevelAccess).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.CustomerId).IsRequired();
            builder.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);

            // Audit Columns
            builder.Property(e => e.Disabled).IsRequired();
            builder.Property(e => e.LastEditor).IsRequired(false);
            builder.Property(e => e.LastEditDate).IsRequired(false);
            builder.Property(e => e.Creator).IsRequired();
            builder.Property(e => e.CreateDate).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            // Relations
        }
    }
}
