using MediUp.Domain.Entities;
using MediUp.Infrastructure.Persistence.Entities;
using MediUp.Infrastructure.Persistence.EntitiesConfiguration;
using MediUp.Infrastructure.Smarter.EntityFramework.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediUp.Infrastructure.Persistence;
public class AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger)
        : SmarterAuditDbContext<CustomAuditLog>(options, logger, GetAuditSettings())
{

    #region Views

    


    #endregion

    #region Properties
    public DbSet<ElectriCompany> ElectriCompany => Set<ElectriCompany>();
    public DbSet<EnergyMeasurementDownload> EnergyMeasurementDownloads => Set<EnergyMeasurementDownload>();
    public DbSet<EnergyMeasurementEvent> EnergyMeasurementEvents => Set<EnergyMeasurementEvent>();
    public DbSet<EnergyMeasurementData> EnergyMeasurementData => Set<EnergyMeasurementData>();
    

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AutoIncrementColumns();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.HasDefaultSchema(DbConstants.Scheme);
        modelBuilder.ApplyConfiguration(new CustomAuditLogTypeConfiguration(nameof(AuditLog), DbConstants.Scheme));
       

        
    }

    private static AuditSettings GetAuditSettings()
    {
        return AuditSettings
            .WithAudit();
        //set here excludes properties
    }

    public bool FindInSet(string value, string stringList)
   => throw new NotSupportedException();

    public static int Locate(string column, string valueList)
        => throw new NotSupportedException();

    public static int Field(string column, params string[] values)
     => throw new NotSupportedException();
}
