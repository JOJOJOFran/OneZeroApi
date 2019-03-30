using OneZero.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Application.Dtos.Permission
{
    public class UserListData:DataDto
    {
        public Guid Id { get; set; }

        public string Account { get; set; }

        public string DisplayName { get; set; }

        public string Roles { get; set; }
    }
}
