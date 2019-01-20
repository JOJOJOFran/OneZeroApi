using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Model.Identity
{
    public class Role:IdentityRole<Guid>
    {
      
        public ICollection<ModuleType> Modules { get; set; }
    }
}