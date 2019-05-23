using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneZero.Core.Models.Permissions
{
    public class Role : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(256)]
        [Description("角色")]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        [Description("角色名称")]
        public string DisplayName { get; set; }

        [MaxLength(2000)]
        [Description("备注")]
        public string Remark { get; set; }

        public IEnumerable<RoleModule> RoleModules { get; set; }

        public IEnumerable<RolePermission> RolePermission { get; set; }




    }
}
