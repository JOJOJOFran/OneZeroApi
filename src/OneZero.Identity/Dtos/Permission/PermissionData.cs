using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permission
{
    public class PermissionData:DataDto
    {

        public Guid ModuleId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }


        public string Remark { get; set; }


        public string ApiPath { get; set; }

        public int RowNo { get; set; }
    }
}
