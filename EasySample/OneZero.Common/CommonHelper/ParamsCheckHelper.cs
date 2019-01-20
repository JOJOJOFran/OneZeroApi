using OneZero.Model.CustomException;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.CommonHelper
{
    public static class ParamsCheckHelper
    {
        public static void NotNull<T>(this T param)
        {
            if (param != null)
                return;
             throw new DefineException(_moduleName + "新增失败", e, ResponseCode.UnExpectedException);
        }
    }
}
