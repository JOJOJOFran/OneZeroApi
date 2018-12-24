using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Lib.Entity.Identity
{
    public class IdentityRoleMoudle<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey RoleId { get; set; }

        public TKey ModleId { get; set; }

        /// <summary>
        /// 存储二进制字符串，0代表有权限，1代表没有权限
        /// </summary>
        public string PermissionValue { get; set; }
    }
}
