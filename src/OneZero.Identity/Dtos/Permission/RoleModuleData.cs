using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class RoleModuleData :DataDto
    {
        public Guid RoleId { get; set; }

        public Guid ModuleId { get; set; }
    }
}
