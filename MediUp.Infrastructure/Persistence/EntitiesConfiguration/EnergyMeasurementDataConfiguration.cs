using MediUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class EnergyMeasurementDataConfiguration : IEntityTypeConfiguration<EnergyMeasurementData>
{
    public void Configure(EntityTypeBuilder<EnergyMeasurementData> builder)
    {
        builder.Property(x => x.ActiveEnergyDeliveredKwh).HasPrecision(18, 4);
        builder.Property(x => x.ActiveEnergyReceivedKwh).HasPrecision(18, 4);
        builder.Property(x => x.ReactiveEnergyDeliveredKvarh).HasPrecision(18, 4);
        builder.Property(x => x.ReactiveEnergyReceivedKvarh).HasPrecision(18, 4);
        builder.Property(x => x.ApparentEnergyDeliveredKvah).HasPrecision(18, 4);
        builder.Property(x => x.IntegrationPeriodSeconds).HasPrecision(18, 4);
        builder.Property(x => x.AverageVoltageKv).HasPrecision(18, 4);
        builder.Property(x => x.Frequency).HasPrecision(18, 4);
        builder.Property(x => x.AveragePowerFactor).HasPrecision(18, 4);
    }
}



