using OneZero.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Lib.Entity.Identity
{
    public class MoudleType<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {

       public TKey ParentId { get; set; }
       
       public string MoudleCode { get; set; }

       public string MoudleName { get; set; }

       public string Description { get; set; }

       public ICollection<MoudleType<TKey>> SubMouldes { get; set; }

       public ICollection<PermissonType<TKey>> Permissons { get; set; }
    }
}
