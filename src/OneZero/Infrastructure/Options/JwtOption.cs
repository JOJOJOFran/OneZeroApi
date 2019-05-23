using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Options
{
    /// <summary>
    /// JWT配置
    /// </summary>
    public class JwtOption
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        //需要大于128字节，所以一般超过16个字符
        public string SecretKey { get; set; }
    }

}
