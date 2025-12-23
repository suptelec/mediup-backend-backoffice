using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Smarter.EntityFramework.Tools;
public class AuditLogTypeConfiguration : AuditLogTypeConfiguration<AuditLog>
{
    public AuditLogTypeConfiguration()
    {
    }

    public AuditLogTypeConfiguration(string tableName, string tableScheme)
        : base(tableName, tableScheme)
    {
    }
}

public class AuditLogTypeConfiguration<TAuditLog> : IEntityTypeConfiguration<TAuditLog> where TAuditLog : BaseAuditLog
{
    private readonly string _tableName;

    private readonly string _tableScheme;

    public AuditLogTypeConfiguration()
    {
        _tableName = "AuditLog";
        _tableScheme = "dbo";
    }

    public AuditLogTypeConfiguration(string tableName, string tableScheme)
    {
        _tableName = tableName;
        _tableScheme = tableScheme;
    }

    public virtual void Configure(EntityTypeBuilder<TAuditLog> builder)
    {
        builder.Ignore((TAuditLog b) => b.OldValues);
        builder.Ignore((TAuditLog b) => b.OldValues);
        builder.Ignore((TAuditLog b) => b.NewValues);
        builder.ToTable(_tableName, _tableScheme);
    }
}
