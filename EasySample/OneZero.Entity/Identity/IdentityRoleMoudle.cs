using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityRoleModule<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey RoleId { get; set; }

        public TKey ModleId { get; set; }

        /// <summary>
        /// 二进制下按顺序，0代表有权限，1代表没有权限
        /// </summary>
        public int PermissionValue { get; set; }
    }
}
