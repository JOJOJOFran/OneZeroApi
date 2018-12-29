using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityUserRole<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey UserId { get; set; }

        public TKey RoleId { get; set; }
    }
}
