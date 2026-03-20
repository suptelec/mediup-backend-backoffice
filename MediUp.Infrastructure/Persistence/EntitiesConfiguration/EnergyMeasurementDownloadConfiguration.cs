using MediUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class EnergyMeasurementDownloadConfiguration : IEntityTypeConfiguration<EnergyMeasurementDownload>
{
    public void Configure(EntityTypeBuilder<EnergyMeasurementDownload> builder)
    {
        builder.HasIndex(e => new { e.Username, e.Meter, e.MeasurementDate })
               .IsUnique()
               .HasDatabaseName("UX_EnergyMeasurementDownload_UserMeterDate");
    }
}


