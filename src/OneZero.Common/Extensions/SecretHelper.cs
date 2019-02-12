using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OneZero.Common.Extensions
{
    public static class SecretHelper
    {
        /// <summary>
        /// 密码MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Hash(this string input)
        {
            using (var md5 = MD5.Create())
            {
                try
                {
                    var strResult = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                    return BitConverter.ToString(strResult).Replace("-", "");
                }
                catch (Exception e)
                {
                    return "";
                }

            }
        }
    }
}
