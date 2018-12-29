﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    /// <summary>
    /// 用户认证实体类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityUser<TKey> :BaseEntity<TKey> where TKey:IEquatable<TKey>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 密码(加密后)
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否邮箱验证
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否手机验证
        /// </summary>
        public bool PhoneConfirmed { get; set; }

        /// <summary>
        /// 账号锁定截止日期
        /// </summary>
        public DateTimeOffset LockoutEnd { get; set; }

        /// <summary>
        /// 是否锁定账号
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 角色集合（导航属性）
        /// </summary>
        public ICollection<IdentityRole<TKey>> IdentityRoles { get; set; }
    }
}
