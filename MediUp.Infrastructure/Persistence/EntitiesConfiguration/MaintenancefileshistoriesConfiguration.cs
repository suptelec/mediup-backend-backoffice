using MediUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediUp.Infrastructure.Persistence.EntitiesConfiguration;
public class MaintenancefileshistoriesConfiguration : IEntityTypeConfiguration<MaintenanceFilesHistory>
{
    public  void Configure(EntityTypeBuilder<MaintenanceFilesHistory> builder)
    {
        builder.Property(e => e.ReportData)
          .HasColumnType("json");
    }
}
