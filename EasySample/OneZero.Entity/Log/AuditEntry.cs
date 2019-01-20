using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using OneZero.Domain.Audits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneZero.EntityFramwork.Log
{
    public class AuditEntry
    {
        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Operation Operation { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();


        public AuditEntry(EntityEntry entry)
        {

        }

        public DbDataOperationAduit ToAudit()
        {
            var audit = new DbDataOperationAduit
            {
                TableName = TableName,
                Operation = Operation,
                DateTime = DateTime.Now,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues=OldValues.Count==0?null: JsonConvert.SerializeObject(OldValues),
                NewValues=NewValues.Count==0?null:JsonConvert.SerializeObject(NewValues)
            };
            return audit;
        }
    }
}
