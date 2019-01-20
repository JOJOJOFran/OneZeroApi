using OneZero.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Model.Identity
{
    public class IdentityModuleType<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        [Description("父模块ID")]
        public Guid ParentId { get; set; }

        [Required]
        [MaxLength(50)]
        [Description("模块")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Description("模块名称")]
        public string DisplayName { get; set; }

        [MaxLength(500)]
        [Description("模块描述")]
        public string Description { get; set; }

    }
}
