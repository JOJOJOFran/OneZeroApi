using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Model.Identity
{
    public class IdentityModulePermission<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        [Required]
        [Description("模块ID")]
        public TKey ModuleId { get; set; }

        [Required]
        [Description("权限ID")]
        public TKey PermissionId { get; set; }

        [Required]
        [Description("序号（唯一")]
        public int SeqNo { get; set; }
    }
}
