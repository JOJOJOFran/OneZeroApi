using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class RolePermissionData:DataDto
    {
        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
