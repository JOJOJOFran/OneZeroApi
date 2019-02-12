using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Repositories
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// 是否提交
        /// </summary>
        bool HasCommited { get; }

        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();

    }
}
