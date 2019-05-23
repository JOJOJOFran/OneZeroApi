using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class AllotRoleModuleData : DataDto
    {
        public List<string> ModuleIds { get; set; }
    }
}
