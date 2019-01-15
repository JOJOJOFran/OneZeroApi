using OneZero.Domain.Audits;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Audits
{
    /// <summary>
    /// 数据库行为审计
    /// 默认实现放在Data层，DefaulfDbActionAudit
    /// </summary>
    public interface IDbActionAudit
    {
        /// <summary>
        /// 在保存之前记录数据库审计
        /// </summary>
        /// <returns></returns>
        ICollection<Audit> RecordBeforSaveAction();

        /// <summary>
        /// 记录查询审计
        /// </summary>
        /// <returns></returns>
        Audit RecordQuery();
    }
}
