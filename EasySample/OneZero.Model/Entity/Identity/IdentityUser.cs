using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneZero.Model.Identity
{
    [Description("用户信息")]
    /// <summary>
    /// 用户认证实体类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityUser<TKey> :BaseEntity<TKey> where TKey:IEquatable<TKey>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(256)]
        [Description("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        [Required]
        [MaxLength(256)]
        [Description("展示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 密码(加密后)
        /// </summary>
        [Required]
        [MaxLength(2000)]
        [Description("密码")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        [MaxLength(2000)]
        [Description("邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 是否邮箱验证
        /// </summary>
        [Description("是否邮箱验证")]
        [DefaultValue(false)]
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Description("手机号")]
        public string Phone { get; set; }

        /// <summary>
        /// 是否手机验证
        /// </summary>
        [Description("是否手机验证")]
        [DefaultValue(false)]
        public bool PhoneConfirmed { get; set; } = false;

        /// <summary>
        /// 账号锁定截止日期
        /// </summary>        
        [Description("账号锁定截止日期")]
        public DateTimeOffset LockoutEnd { get; set; }

        /// <summary>
        /// 是否锁定账号
        /// </summary>
        [Description("是否锁定账号")]
        [DefaultValue(false)]
        public bool LockoutEnabled { get; set; }
    }
}
