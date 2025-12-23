using MediUp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Persistence.Interceptors;
public class AuditInterceptor(IServiceProvider serviceProvider) : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AuditEntities(eventData.Context);
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AuditEntities(eventData.Context);
        return ValueTask.FromResult(result);
    }

    private void AuditEntities(DbContext? context)
    {
        if (context == null) return;

        var now = DateTime.Now;
        var currentUser = serviceProvider.GetRequiredService<ICurrentLoggedUser>();
        var currentUserName = currentUser?.UserName ?? "System";

        var entries = context.ChangeTracker.Entries<IBaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = string.IsNullOrWhiteSpace(entry.Entity.CreatedBy) ? currentUserName : entry.Entity.CreatedBy;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = currentUserName;
                    break;
                default:
                    break;
            }
        }
    }
}
