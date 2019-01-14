using OneZero.Domain.Audits;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Audits
{
    /// <summary>
    /// 使用Redis记录审计日志
    /// </summary>
    public interface IAuditByRedis
    {
        /// <summary>
        /// 是否开启
        /// </summary>
        bool AuditByRedisIsOpen { get; }

        /// <summary>
        /// 记录审计日志
        /// </summary>
        /// <param name="audit">日志实体类</param>
        /// <returns>是否成功（这里的异常不应该影响整个业务流程的运转，可计入Warning级别日志）</returns>
        bool Record(Audit audit);
    }
}
