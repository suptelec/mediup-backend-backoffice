using MediUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class PowerFactorThresholdConfiguration : IEntityTypeConfiguration<PowerFactorThreshold>
{
    public void Configure(EntityTypeBuilder<PowerFactorThreshold> builder)
    {
        builder.ToTable("PowerFactorThresholds");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Level).HasMaxLength(20).IsRequired();
        builder.Property(x => x.MinValue).HasColumnType("decimal(4,2)");
        builder.Property(x => x.MaxValue).HasColumnType("decimal(4,2)");
        builder.Property(x => x.Description).HasMaxLength(100).IsRequired();

        builder.HasData(
            new PowerFactorThreshold { Id = 1, Level = "Critical", MinValue = null, MaxValue = 0.60m, Description = "Factor de potencia cr\u00EDtico",CreatedBy="System" },
            new PowerFactorThreshold { Id = 2, Level = "Low", MinValue = 0.61m, MaxValue = 0.94m, Description = "Bajo - Penalizado", CreatedBy = "System" },
            new PowerFactorThreshold { Id = 3, Level = "Normal", MinValue = 0.95m, MaxValue = 1.00m, Description = "Normal", CreatedBy = "System" }
        );
    }
}
