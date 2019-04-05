using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OneZero.Core.Models.Permissions
{
    public class User : BaseEntity<Guid>
    {
        #region 字段
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(256)]
        [Description("用户名")]
        public string Account { get; set; }

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


        /// <summary>
        /// 部门Id
        /// </summary>
        public Guid DepartmentId { get; set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 角色集合
        /// </summary>
        public IEnumerable<UserRole> UserRoles { get; set; }
        #endregion
    }
}
