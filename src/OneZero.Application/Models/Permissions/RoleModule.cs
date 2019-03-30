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
        public Guid ModuleId { get; set; }

        [Description("行号")]
        public int RowNo { get; set; }

    }
}
