using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Domain.Audits
{
    /// <summary>
    /// 数据操作审计日志类型
    /// </summary>
    public class DbDataOperationAduit: Audit
    {

        public string TableName { get; set; }


        public string OperationString {
            get { return Operation.ToString(); }
            private set { Operation = (Operation)Enum.Parse(typeof(Operation), value, true); }
        }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public string CommandSql { get; set; }

        //[NotMapped]
        public Operation Operation { get; set; }
    }

    public enum Operation
    {
        Add=0,
        Delete=1,
        Modified=2
    }
}
