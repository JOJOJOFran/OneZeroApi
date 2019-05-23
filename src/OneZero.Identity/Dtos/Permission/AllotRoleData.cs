using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class AllotRoleData : DataDto
    {
        public List<string> RoleIds { get; set; }
    }
}
