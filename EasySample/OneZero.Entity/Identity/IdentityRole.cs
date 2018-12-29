using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityRole<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }

        public string Remark { get; set; }

        public ICollection<IdentityModuleType<TKey>> Modules { get; set; }
    }
}
