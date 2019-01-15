using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Audits
{
    public class DbRequestAudit:Audit
    {
        public string DbName { get; set; }
        
        public string TransName { get; set; }

        public string CommandSql { get; set; }
    }
}
