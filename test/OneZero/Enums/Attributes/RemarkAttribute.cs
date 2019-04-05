using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Enums
{

    public class RemarkAttribute:Attribute
    {
        public string Remark { get;  }

        public RemarkAttribute(string remark)
        {
            Remark = remark;
        }

        public string GetRemark()
        {
            return Remark;
        }
    }
}
