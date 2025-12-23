using MediUp.Infrastructure.Persistence.Entities;
using MediUp.Infrastructure.Smarter.EntityFramework.Tools;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class CustomAuditLogTypeConfiguration(string tableName, string tableScheme) : AuditLogTypeConfiguration<CustomAuditLog>(tableName, tableScheme)
{
    public override void Configure(EntityTypeBuilder<CustomAuditLog> builder)
    {
        base.Configure(builder);
        builder.HasIndex(b => b.AuditUser);
        builder.HasIndex(b => b.Id);
        builder.HasIndex(b => b.EntityType);

        builder.Property(b => b.IpAddress).HasMaxLength(50);
        builder.HasIndex(b => b.IpAddress);
        builder.HasIndex(b => b.AuditDate);
        builder.HasIndex(b => b.AuditUser);
        builder.HasIndex(b => new { b.EntityType, b.EntityKey });
        builder.Property(b => b.Action).HasMaxLength(50);
        builder.Property(b => b.AuditUser).HasMaxLength(150);
        builder.Property(b => b.EntityType).HasMaxLength(100);
        builder.Property(b => b.ModuleId).HasMaxLength(50);
        builder.HasIndex(b => b.ModuleId);
    }
}
