using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
    public class Role:IdentityRole<Guid>
    {
      
        public ICollection<ModuleType> Modules { get; set; }
    }
}