using OneZero.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityPermissionType<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public string PermissionName { get; set; }

        public string Remark { get; set; }

        public int RowNo { get; set; }
    }
}
