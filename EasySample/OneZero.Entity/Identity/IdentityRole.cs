using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Lib.Entity.Identity
{
    public class IdentityRole<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public string Name { get; set; }

        public string Remark { get; set; }

        public ICollection<MoudleType<TKey>> Moudles { get; set; }
    }
}
