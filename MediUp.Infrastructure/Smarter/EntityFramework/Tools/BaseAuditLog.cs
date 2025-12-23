using Audit.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Smarter.EntityFramework.Tools;
public abstract class BaseAuditLog
{
    public long Id { get; set; }

    public string EntityKey { get; set; }

    public string OldData { get; set; }

    public string NewData { get; set; }

    public string EntityType { get; set; }

    public DateTime AuditDate { get; set; } = DateTime.UtcNow;


    public string AuditUser { get; set; }

    public string Action { get; set; }

    public IDictionary<string, object> OldValues { get; set; } = new Dictionary<string, object>();


    public IDictionary<string, object> NewValues { get; set; } = new Dictionary<string, object>();


    public virtual void ToAudit(EventEntry entry, IAuditSettings config)
    {
        EntityType = entry.EntityType.Name;
        Action = entry.Action;
        EntityKey = string.Join(",", entry.PrimaryKey.Select((KeyValuePair<string, object> x) => x.Value.ToString()).ToArray());
        foreach (var (tableName, list2) in config.IgnoredProps)
        {
            IgnoreProperties(tableName, list2.ToArray());
        }

        OldData = ((OldValues.Count == 0) ? null : JsonSerializer.Serialize(OldValues));
        NewData = ((NewValues.Count == 0) ? null : JsonSerializer.Serialize(NewValues));
        string text2 = ((entry.Action == "Insert") ? config.CreatedBy : config.UpdatedBy);
        AuditUser = ((NewValues.Keys.Contains(text2) && NewValues[text2] != null) ? NewValues[text2].ToString() : string.Empty);
        config.SetCustomProperties?.Invoke(this, entry.Entity ?? entry.GetEntry()?.Entity);
    }

    public virtual void IgnoreProperties(string tableName, params string[] props)
    {
        if (props == null || props.Length == 0)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(tableName))
        {
            throw new ArgumentNullException("tableName", "You need to provide a table name");
        }

        if (tableName != EntityType)
        {
            return;
        }

        foreach (string key in props)
        {
            if (OldValues.ContainsKey(key))
            {
                OldValues.Remove(key);
            }

            if (NewValues.ContainsKey(key))
            {
                NewValues.Remove(key);
            }
        }
    }
}
