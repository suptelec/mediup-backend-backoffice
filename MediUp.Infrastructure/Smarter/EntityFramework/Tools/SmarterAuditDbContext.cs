using Audit.EntityFramework;
using MediUp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Smarter.EntityFramework.Tools;
public class SmarterAuditDbContext<TAuditLog> : AuditDbContext where TAuditLog : BaseAuditLog
{
    protected readonly ILogger Logger;

    private readonly IAuditSettings _config;

    public virtual DbSet<TAuditLog> AuditLog { get; set; }

    public SmarterAuditDbContext(DbContextOptions options, ILogger logger, IAuditSettings config)
        : base(options)
    {
        Logger = logger;
        _config = config;
        _config.CheckConfig(withSequence: false, withAudit: true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        IAuditSettings config = _config;
        if (config.SetCustomProperties == null)
        {
            Action<BaseAuditLog, object> action2 = (config.SetCustomProperties = delegate (BaseAuditLog auditEntity, object entity)
            {
                SetCustomProperties(auditEntity as TAuditLog, entity);
            });
        }

        modelBuilder.WithAudit<TAuditLog>(Logger, _config);
        if (typeof(TAuditLog) == typeof(AuditLog))
        {
            modelBuilder.ApplyConfiguration(new AuditLogTypeConfiguration("AuditLog", DbConstants.Scheme));
        }
    }

    public override int SaveChanges()
    {
        ChangeTracker.Entries().SetDatabaseValues(Logger);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        ChangeTracker.Entries().SetDatabaseValues(Logger);
        return base.SaveChangesAsync(cancellationToken);
    }

    public virtual void SetCustomProperties(TAuditLog auditLog, object entity)
    {
    }
}