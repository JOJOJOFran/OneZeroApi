﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        Task<int> CommitAsync();

        /// <summary>
        /// 回滚(暂时不实现)
        /// </summary>
        Task RollbackAsync();

    }
}
