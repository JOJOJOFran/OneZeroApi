using OneZero.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Lib.Entity.Identity
{
    public class PermissonType<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public string PermissionName { get; set; }

        public string Remark { get; set; }

        public int RowNo { get; set; }
    }
}
