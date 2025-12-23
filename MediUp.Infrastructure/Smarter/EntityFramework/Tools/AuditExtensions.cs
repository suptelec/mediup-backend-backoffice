using Audit.Core;
using Audit.EntityFramework;
using Audit.EntityFramework.ConfigurationApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Smarter.EntityFramework.Tools;
internal static class AuditExtensions
{
    public static void WithAudit(this ModelBuilder modelBuilder, ILogger logger, IAuditSettings config)
    {
        modelBuilder.WithAudit<AuditLog>(logger, config, withSequence: false);
    }

    public static void WithAudit<TAuditLog>(this ModelBuilder modelBuilder, ILogger logger, IAuditSettings config) where TAuditLog : BaseAuditLog
    {
        modelBuilder.WithAudit<TAuditLog>(logger, config, withSequence: false);
    }

    public static void WithAudit<TAuditLog>(this ModelBuilder modelBuilder, ILogger logger, IAuditSettings config, bool withSequence) where TAuditLog : BaseAuditLog
    {
        Audit.Core.Configuration.Setup().UseEntityFramework(delegate (IEntityFrameworkProviderConfigurator c)
        {
            c.AuditTypeMapper((Type _) => typeof(TAuditLog)).AuditEntityAction((AuditEvent _, EventEntry entry, TAuditLog entity) => AuditEntity(entry, entity, logger, config)).IgnoreMatchedProperties();
        });
        if (withSequence)
        {
            modelBuilder.WithSequence(logger, config);
        }
    }

    public static void SetDatabaseValues(this IEnumerable<EntityEntry> changes, ILogger logger)
    {
        logger.LogInformation("SetDatabaseValues: Setting entity values for modified entities...");
        try
        {
            foreach (EntityEntry change in changes)
            {
                object entity = change.Entity;
                if ((entity == null || !entity.GetType().IsSubclassOf(typeof(BaseAuditLog))) && change.State != 0 && change.State != EntityState.Unchanged && change.State == EntityState.Modified)
                {
                    change.OriginalValues.SetValues(change.GetDatabaseValues());
                }
            }

            logger.LogInformation("SetDatabaseValues: Process completed");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "SetDatabaseValues: Unknown error occurred");
        }
    }

    public static void CheckConfig(this IAuditSettings config, bool withSequence, bool withAudit)
    {
        if (config == null)
        {
            throw new ArgumentNullException("config", "You need to provide a valid config");
        }

        if (withAudit)
        {
            if (string.IsNullOrEmpty(config.CreatedBy))
            {
                throw new ArgumentNullException("CreatedBy", "You need to provide a valid 'created by'");
            }

            if (string.IsNullOrEmpty(config.UpdatedBy))
            {
                throw new ArgumentNullException("CreatedBy", "You need to provide a valid 'updated by'");
            }
        }

        if (withSequence)
        {
            if (config.Node <= 0)
            {
                throw new ArgumentNullException("Node", "You need to provide a valid node");
            }

            if (string.IsNullOrEmpty(config.Assembly))
            {
                throw new ArgumentNullException("Assembly", "You need to provide an assembly name");
            }

            if (string.IsNullOrEmpty(config.ModelNameSpace))
            {
                throw new ArgumentNullException("ModelNameSpace", "You need to provide the model namespace");
            }
        }
    }

    public static void WithSequence(this ModelBuilder modelBuilder, ILogger logger, IAuditSettings config)
    {
        long node = config.Node;
        string assembly = config.Assembly;
        string modelNameSpace = config.ModelNameSpace;
        logger.LogInformation(string.Format("{0}: Sequence node = {1} - Assembly = {2} - Models = {3}", "WithSequence", node, assembly, modelNameSpace));
        Assembly assembly2 = Assembly.Load(assembly);
        logger.LogInformation("Assembly to load: " + assembly2.FullName);
        if (assembly2 == null)
        {
            throw new Exception("Unable to load assembly " + assembly);
        }

        List<Type> typesInNamespace = GetTypesInNamespace(Assembly.Load(assembly), modelNameSpace);
        if (!typesInNamespace.Any())
        {
            logger.LogWarning("WithSequence: Model classes not found on assembly: " + assembly + ", namespace: " + modelNameSpace);
            return;
        }

        foreach (Type item in typesInNamespace)
        {
            if (HasIdSequenceAttribute(item, out var propertyName))
            {
                string name = item.Name;
                string text = $"CONVERT(bigint, ('{node}' + CAST(NEXT VALUE FOR seq.{name} as nvarchar(255))))";
                logger.LogInformation("WithSequence: Creating sequence for model class: " + item.Name + ". Sql = " + text);
                modelBuilder.HasSequence<long>(name, "seq").StartsAt(1L).IncrementsBy(1);
                modelBuilder.Entity(item).Property(propertyName).HasDefaultValueSql(text);
                logger.LogInformation("WithSequence: Sequence created successfully for model class: " + item.Name);
            }
        }
    }

    private static bool AuditEntity(EventEntry entry, BaseAuditLog entity, ILogger logger, IAuditSettings config)
    {
        logger.LogInformation("AuditEntity: Auditing entity...");
        try
        {
            if (entry.EntityType.IsSubclassOf(typeof(BaseAuditLog)))
            {
                return false;
            }

            switch (entry.Action)
            {
                case "Insert":
                    entity.NewValues = entry.ColumnValues;
                    break;
                case "Update":
                    foreach (EventEntryChange change in entry.Changes)
                    {
                        entity.OldValues.Add(change.ColumnName, change.OriginalValue);
                    }

                    entity.NewValues = entry.ColumnValues;
                    break;
                case "Delete":
                    entity.OldValues = entry.ColumnValues;
                    break;
            }

            entity.ToAudit(entry, config);
            logger.LogInformation("AuditEntity: Entity = " + entity.EntityType + " was audited");
            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "AuditEntity: Unknown error occurred");
        }

        return false;
    }

    private static bool HasIdSequenceAttribute(Type type, out string propertyName)
    {
        propertyName = string.Empty;
        PropertyInfo propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault((PropertyInfo p) => p.GetCustomAttributes(typeof(IdSequenceAttribute), inherit: true).Length != 0);
        if (propertyInfo == null)
        {
            return false;
        }

        propertyName = propertyInfo.Name;
        return true;
    }

    private static List<Type> GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return (from t in assembly.GetTypes()
                where t.IsClass && !string.IsNullOrEmpty(t.Namespace) && t.Namespace.StartsWith(nameSpace)
                select t).ToList();
    }
}