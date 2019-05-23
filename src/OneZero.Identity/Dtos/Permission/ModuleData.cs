using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Core.Dtos.Permissions
{
    public class ModuleData:DataDto
    {
        public Guid? ParentId { get; set; }
        public string Name { get; set; }

        public string Path { get; set; }

        public string DisplayName { get; set; }


        public string Description { get; set; }
    }
}
