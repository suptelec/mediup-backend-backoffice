using MediUp.Domain.Entities;
using MediUp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure;
public static class Seed
{
    public static async Task SeedApp(AppDbContext context)
    {
        const string createdBy = "seed";

        var countries = new List<ElectriCompany>
        {
            new() {
                Name = "España",
                TaxId = "ES-SEED-001",
                Country = "España",
                ContactPhone = "+34",
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            }
        };

        context.ElectriCompanies.AddRange(countries);

      
        await context.SaveChangesAsync();
    }
}
