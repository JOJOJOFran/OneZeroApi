using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Lib.Entity.Identity
{
    public class MoudlePermisson<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey NoudleId { get; set; }

        public TKey PermissonId { get; set; }
    }
}
