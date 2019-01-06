using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Log
{
    public class AduitDb:BaseEntity<Guid>
    {
        public string TableName { get; set; }

        public string EntityName { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public Operation Operation { get; set; }
    }

    public enum Operation
    {
        Add=0,
        Delete=1,
        Update=2
    }
}
