using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Models
{
    /// <summary>
    /// 数据实体接口
    /// out:协变=》可以变更为它的基类，仅用于输出
    /// IEquatable:比较对象相等性
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IEntity<out TKey> 
    {
        TKey Id { get;  }

        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDelete { get; set; }
    }
}
