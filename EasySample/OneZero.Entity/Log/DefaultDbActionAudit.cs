using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OneZero.Domain.Audits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneZero.Entity.Log
{
    public class DefaultDbActionAudit : IDbActionAudit
    {
        DbContext _dbContext;
        List<DbDataOperationAduit> operationAduits=new List<DbDataOperationAduit>();
        DbRequestAudit requestAudit;

        public DefaultDbActionAudit(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Audit> RecordBeforSaveAction()
        {

            _dbContext.ChangeTracker.DetectChanges();
            var audits = new List<AuditEntry>();

            foreach (var item in _dbContext.ChangeTracker.Entries())
            {
                if (item.Entity is Audit || item.State == EntityState.Detached || item.State == EntityState.Unchanged)
                    continue;

                var audit = new AuditEntry(item)
                {
                    TableName = item.Metadata.Relational().TableName
                };

                audits.Add(audit);

                foreach (var property in item.Properties)
                {
                    if (property.IsTemporary)
                    {
                        //在保存之后，通过数据库自动生成
                        audit.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        audit.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (item.State)
                    {
                        case EntityState.Added:
                            audit.Operation = Operation.Add;
                            audit.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            audit.Operation = Operation.Delete;
                            audit.OldValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                audit.Operation = Operation.Modified;
                                audit.OldValues[propertyName] = property.OriginalValue;
                                audit.NewValues[propertyName] = property.CurrentValue;
                            }                                                                        
                            break;
                    }
                }

                //保存所有已经修改的Audit实体
                foreach (var opAudit in audits.Where(_ => _.HasTemporaryProperties))
                {
                    operationAduits.Add(opAudit.ToAudit());
                }

                return (ICollection<Audit>)operationAduits;

            }
            

            return null;
        }

        public Audit RecordQuery()
        {
            requestAudit = new DbRequestAudit
            {
                Id = Guid.NewGuid().ToString(),
                DateTime = DateTime.Now,
                CommandSql = DbLoggerCategory.Database.Command.Name,
                DbName = DbLoggerCategory.Database.Name,
                TransName = DbLoggerCategory.Database.Transaction.Name
            };

            return requestAudit;
        }
    }
}
