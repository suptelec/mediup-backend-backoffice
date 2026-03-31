using MediUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class VoltageThresholdConfiguration : IEntityTypeConfiguration<VoltageThreshold>
{
    public void Configure(EntityTypeBuilder<VoltageThreshold> builder)
    {
        builder.ToTable("VoltageThresholds");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NominalKv).HasColumnType("decimal(5,1)").IsRequired();
        builder.Property(x => x.LowerNormal).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(x => x.LowerEmergent).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(x => x.UpperNormal).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(x => x.UpperEmergent).HasColumnType("decimal(5,2)").IsRequired();

        builder.HasData(
            new VoltageThreshold { Id = 1, NominalKv = 138m, LowerNormal = -0.05m, LowerEmergent = -0.06m, UpperNormal = 0.05m, UpperEmergent = 0.06m, CreatedBy = "System" },
            new VoltageThreshold { Id = 2, NominalKv = 22m, LowerNormal = -0.03m, LowerEmergent = -0.05m, UpperNormal = 0.04m, UpperEmergent = 0.06m, CreatedBy = "System" },
            new VoltageThreshold { Id = 3, NominalKv = 6.6m, LowerNormal = -0.03m, LowerEmergent = 0.00m, UpperNormal = 0.04m, UpperEmergent = 0.00m , CreatedBy = "System" },
            new VoltageThreshold { Id = 4, NominalKv = 480m, LowerNormal = -0.05m, LowerEmergent = -0.06m, UpperNormal = 0.05m, UpperEmergent = 0.06m , CreatedBy = "System" },
            new VoltageThreshold { Id = 5, NominalKv = 220m, LowerNormal = -0.05m, LowerEmergent = -0.06m, UpperNormal = 0.05m, UpperEmergent = 0.06m, CreatedBy = "System" }
        );
    }
}