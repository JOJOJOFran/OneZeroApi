using OneZero.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Application.Models
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class BaseEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public TKey TenanId { get; set; } = default(TKey);

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; } = false;

        /// <summary>
        /// 重写Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is BaseEntity<TKey> entity))
                return false;

            if (entity.Id == null && Id == null)
                return true;

            if (entity.Id == null || Id == null)
                return false;

            return Id.Equals(entity.Id);
        }

        /// <summary>
        /// 重写HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (Id == null)
            {
                return 0;
            }
            return Id.ToString().GetHashCode();
        }

    }
}
