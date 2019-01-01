using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityUserRole<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        [Required]
        [Description("用户ID")]
        public TKey UserId { get; set; }

        [Required]
        [Description("角色ID")]
        public TKey RoleId { get; set; }
    }
}
