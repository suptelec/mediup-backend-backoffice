using MediUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Persistence;
internal class DbConstants
{
    public const string Scheme = "mup";
    public const string MigrationsTableName = "_ef_migrations";

    public const string SelectForUpdateTag = "ForUpdate";

    public static string GenerateTableSchema(string schema, string entityName)
        => $"{schema}_{entityName}".ToLower();

    public static readonly Dictionary<Type, List<PropertyInfo>> TypesWithDecimals = typeof(BaseEntity)
        .Assembly.GetTypes()
        .Where(t => t.IsSubclassOf(typeof(BaseEntity)) && t is { IsAbstract: false, IsPublic: true })
        .ToDictionary(type => type, type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?)).ToList());
}
