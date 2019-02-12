using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Application.Models.Permissions
{
    public class ModuleType:BaseEntity<Guid>
    {
        [Description("父模块ID")]
        public Guid ParentId { get; set; }

        [Required]
        [MaxLength(50)]
        [Description("模块")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [Description("模块路径")]
        public string Path { get; set; }

        [Required]
        [MaxLength(50)]
        [Description("模块名称")]
        public string DisplayName { get; set; }

        [MaxLength(500)]
        [Description("模块描述")]
        public string Description { get; set; }


        public ICollection<ModuleType> SubMouldes { get; set; }

        public ICollection<PermissionType> Permissions { get; set; }
    }
}
