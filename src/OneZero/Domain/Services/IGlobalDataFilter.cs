using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OneZero.Domain.Services
{
    /// <summary>
    /// 全局过滤设置
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="Tkey"></typeparam>
    public interface IGlobalDataFilter<TEntity,Tkey>
    {
        Expression<Func<TEntity, bool>> SetGloablDataFilter();
    }
}
