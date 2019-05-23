using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class UserRoleData : DataDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

    }
}
