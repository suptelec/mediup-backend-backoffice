using MediUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class LigtherMetricConfiguration : IEntityTypeConfiguration<LigtherMetric>
{
    public void Configure(EntityTypeBuilder<LigtherMetric> builder)
    {
        builder.Property(x => x.Latitude).HasPrecision(9, 6);
        builder.Property(x => x.Longitude).HasPrecision(9, 6);
        builder.Property(x => x.NominalKv).HasColumnType("decimal(5,1)");

    }
}
