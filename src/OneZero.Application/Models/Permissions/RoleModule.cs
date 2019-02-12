using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace OneZero.Application.Models.Permissions
{
    public class RoleModule : BaseEntity<Guid>
    {
        [Description("角色ID")]
        public Guid RoleId { get; set; }

        [Description("模块ID")]
        public Guid ModleId { get; set; }

        [Description("行号")]
        public int RowNo { get; set; }
        /// <summary>
        /// 二进制下按顺序，0代表有权限，1代表没有权限
        /// </summary>
        //[Description("角色对应权限值")]
        //[DefaultValue(0)]
       // public int PermissionValue { get; set; }

    }
}
