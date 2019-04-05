using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Core.Models.Permissions
{
    public class RolePermission : BaseEntity<Guid>
    {
        [Required]
        [Description("角色ID")]
        public Guid RoleId { get; set; }

        [Required]
        [Description("模块ID")]
        public Guid ModuleId { get; set; }

        [Required]
        [Description("权限ID")]
        public Guid PermissionId { get; set; }

        [Required]
        [Description("序号（唯一")]
        public int SeqNo { get; set; }
    }
}
