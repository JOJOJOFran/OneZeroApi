using OneZero.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Model.Identity
{
    public class IdentityPermissionType<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
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

        [Required]
        [Description("行号")]
        public int RowNo { get; set; }
    }
}
