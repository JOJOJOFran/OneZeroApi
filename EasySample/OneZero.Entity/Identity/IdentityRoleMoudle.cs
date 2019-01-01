using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityRoleModule<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        [Description("角色ID")]
        public TKey RoleId { get; set; }

        [Description("模块ID")]
        public TKey ModleId { get; set; }

        /// <summary>
        /// 二进制下按顺序，0代表有权限，1代表没有权限
        /// </summary>
        [Description("角色对应权限值")]
        [DefaultValue(0)]
        public int PermissionValue { get; set; }
    }
}
