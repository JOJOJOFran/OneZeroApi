using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityModulePermission<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey NoudleId { get; set; }

        public TKey PermissionId { get; set; }
    }
}
