using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class UserData:DataDto
    {
        public Guid? Id { get; set; }

        public string Account { get; set; }

        public string DisplayName { get; set; }


        public Guid? RoleId { get; set; }


        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 密码字符串
        /// </summary>
        public string Password { get; set; }

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
        public DateTimeOffset LockoutEnd { get; set; } = DateTime.Now.AddYears(100);

        /// <summary>
        /// 是否锁定账号
        /// </summary>

        public bool LockoutEnabled { get; set; } = false;
    }
}
