using OneZero.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class IdentityModuleType<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {

       public TKey ParentId { get; set; }
       
       public string ModuleCode { get; set; }

       public string ModuleName { get; set; }

       public string Description { get; set; }

       public ICollection<IdentityModuleType<TKey>> SubMouldes { get; set; }

       public ICollection<IdentityPermissionType<TKey>> Permissions { get; set; }
    }
}
