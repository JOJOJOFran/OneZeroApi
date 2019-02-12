using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Application.Models.Permissions
{
    public class UserRole : BaseEntity<Guid>
    {
        [Required]
        [Description("用户ID")]
        public Guid UserId { get; set; }

        [Required]
        [Description("角色ID")]
        public Guid RoleId { get; set; }

        [Description("行号")]
        public int RowNo { get; set; }
    }
}
