using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class UserUpdateData:DataDto
    {
        public string Account { get; set; }

        public string DisplayName { get; set; }


        public Guid? RoleId { get; set; }


        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
    }
}
