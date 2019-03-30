using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero
{
    public class OneZeroContext:IOneZeroContext
    {
        public Guid TenanId { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<string> PermissionList { get; set; }
    }
}
