using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Application.Models.Permissions
{
    public class PermissionType : BaseEntity<Guid>
    {
        [Required]
        [MaxLength(50)]
        [Description("权限")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Description("权限名称")]
        public string DisplayName { get; set; }

        [MaxLength(50)]
        [Description("备注")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Description("Api路径")]
        public string ApiPath { get; set; }

        [Required]
        [Description("行号")]
        public int RowNo { get; set; }
    }
}
