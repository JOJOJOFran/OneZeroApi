using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Model.Input.Identity
{
    public class RoleItemInput:InputModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Remark { get; set; }
    }
}
