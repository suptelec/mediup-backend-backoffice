using MediUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class EnergyMeasurementSummaryConfiguration : IEntityTypeConfiguration<EnergyMeasurementSummary>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EnergyMeasurementSummary> builder)
    {
        builder.Property(x => x.TotalActiveEnergyDeliveredKwh).HasPrecision(18, 4);
        builder.Property(x => x.TotalActiveEnergyReceivedKwh).HasPrecision(18, 4);
        builder.Property(x => x.TotalReactiveEnergyDeliveredKvarh).HasPrecision(18, 4);
        builder.Property(x => x.TotalReactiveEnergyReceivedKvarh).HasPrecision(18, 4);
        builder.Property(x => x.TotalApparentEnergyDeliveredKvah).HasPrecision(18, 4);
    }
}
