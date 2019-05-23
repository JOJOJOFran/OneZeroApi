using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Enums
{
    /// <summary>
    /// 请求响应结果码枚举值
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        Success = 0,


        /// <summary>
        /// 意料中的异常
        /// </summary>
        ExpectedException = 1,

        /// <summary>
        /// 不可预料的异常
        /// </summary>
        UnExpectedException = 2,

        /// <summary>
        /// 内部错误
        /// </summary>
        Error = 3,

        /// <summary>
        /// 灾难性错误
        /// </summary>
        Fatal = 4,

        /// <summary>
        /// 未授权
        /// </summary>
        UnAuthorize = 4011,

        /// <summary>
        /// Token过期
        /// </summary>
        TokenExpire = 4012,

        /// <summary>
        /// 异地登陆
        /// </summary>
        IpNotMatch = 4013,


        /// <summary>
        /// 非法请求
        /// </summary>
        IllegalRequest = 4014,

    }
}
