using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneZero.Enums
{
    public enum Gender
    {
        [Remark("女")]
        Female=0,
        [Remark("男")]
        Male = 1
    }
}
