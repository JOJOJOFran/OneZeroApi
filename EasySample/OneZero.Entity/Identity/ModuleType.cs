using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class ModuleType:IdentityModuleType<Guid>
    {
   

        public ICollection<ModuleType> SubMouldes { get; set; }

        public ICollection<PermissionType> Permissions { get; set; }
    }
}