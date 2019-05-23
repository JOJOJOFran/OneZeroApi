using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class AllotRolePermissionData : DataDto
    {
        public List<ModulePermissionItem> PermissioIds { get; set; }
    }

    public class ModulePermissionItem
    {
        public Guid ModuleId { get; set; }

        public Guid PermissioId { get; set; }

    }
}
