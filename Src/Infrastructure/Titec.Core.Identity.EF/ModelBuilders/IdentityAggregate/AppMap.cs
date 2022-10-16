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
    public class AppMap : IEntityTypeConfiguration<AppEntity>
    {
        public void Configure(EntityTypeBuilder<AppEntity> builder)
        {
            builder.ToTable("Apps");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            // Columns
            builder.Property(e => e.Title).IsRequired().HasMaxLength(500);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);

            // Audit Columns
            builder.Property(e => e.Disabled).IsRequired();
            builder.Property(e => e.LastEditor).IsRequired(false);
            builder.Property(e => e.LastEditDate).IsRequired(false);
            builder.Property(e => e.Creator).IsRequired();
            builder.Property(e => e.CreateDate).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
        }
    }
}
