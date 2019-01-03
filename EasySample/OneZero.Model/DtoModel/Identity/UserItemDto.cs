using System;
using System.Collections.Generic;

namespace OneZero.Model.DtoModel
{
    public class UserItemDto:IDtoData
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
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否手机验证
        /// </summary>
        public bool PhoneConfirmed { get; set; } = false;

        /// <summary>
        /// 账号锁定截止日期
        /// </summary>        
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// 是否锁定账号
        /// </summary>
        public bool LockoutEnabled { get; set; }= false;
    }
}