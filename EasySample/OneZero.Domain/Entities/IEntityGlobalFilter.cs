using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Entities
{
    /// <summary>
    /// 实体全局过滤接口
    /// 例如：租户ID IsDelete过滤
    /// </summary>
    public interface IEntityGlobalFilter<TEntity,TKey> where TEntity: IEntity<TKey> where TKey :IEquatable<TKey>
    {
        Func<IEntity<TKey>, bool>  SetGlobalFilter(TEntity entity);
    }
}
