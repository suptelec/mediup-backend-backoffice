using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Smarter.EntityFramework.Tools;
public class AuditSettings : IAuditSettings
{
    public long Node { get; }

    public string Assembly { get; }

    public string ModelNameSpace { get; }

    public string CreatedBy { get; }

    public string UpdatedBy { get; }

    public Action<BaseAuditLog, object> SetCustomProperties { get; set; }

    public Dictionary<string, List<string>> IgnoredProps { get; } = new Dictionary<string, List<string>>();


    private AuditSettings(string createdBy, string updatedBy, Action<BaseAuditLog, object> setCustomProps)
    {
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        SetCustomProperties = setCustomProps;
    }

    private AuditSettings(long node, string assembly, string modelNameSpace, Action<BaseAuditLog, object> setCustomProps)
    {
        Node = node;
        Assembly = assembly;
        ModelNameSpace = modelNameSpace;
        SetCustomProperties = setCustomProps;
    }

    private AuditSettings(long node, string assembly, string modelNameSpace, string createdBy, string updatedBy, Action<BaseAuditLog, object> setCustomProps)
    {
        Node = node;
        Assembly = assembly;
        ModelNameSpace = modelNameSpace;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        SetCustomProperties = setCustomProps;
    }

    public static AuditSettings WithAudit()
    {
        return WithAudit("CreatedBy", "UpdatedBy");
    }

    public static AuditSettings WithAudit(string createdBy, string updatedBy, Action<BaseAuditLog, object> setCustomProps = null)
    {
        AuditSettings auditSettings = new AuditSettings(createdBy ?? "CreatedBy", updatedBy ?? "UpdatedBy", setCustomProps);
        auditSettings.CheckConfig(withSequence: false, withAudit: true);
        return auditSettings;
    }

    public static AuditSettings WithAudit(Action<BaseAuditLog, object> setCustomProps)
    {
        return WithAudit("CreatedBy", "UpdatedBy", setCustomProps);
    }

    public static AuditSettings WithSequence(long node, string assembly, string modelNameSpace)
    {
        AuditSettings auditSettings = new AuditSettings(node, assembly, modelNameSpace, null);
        auditSettings.CheckConfig(withSequence: true, withAudit: false);
        return auditSettings;
    }

    public static AuditSettings WithAuditAndSequence(long node, string assembly, string modelNameSpace, string createdBy, string updatedBy)
    {
        AuditSettings auditSettings = new AuditSettings(node, assembly, modelNameSpace, createdBy, updatedBy, null);
        auditSettings.CheckConfig(withSequence: true, withAudit: true);
        return auditSettings;
    }

    public AuditSettings WithIgnoredProps(string tableName, params string[] props)
    {
        if (string.IsNullOrWhiteSpace(tableName))
        {
            throw new ArgumentNullException("tableName");
        }

        if (props == null || props.Length == 0)
        {
            throw new ArgumentOutOfRangeException("props");
        }

        if (IgnoredProps.ContainsKey(tableName))
        {
            IgnoredProps.First((KeyValuePair<string, List<string>> x) => x.Key == tableName).Value.AddRange(props.ToList());
        }
        else
        {
            IgnoredProps.Add(tableName, props.ToList());
        }

        return this;
    }
}
