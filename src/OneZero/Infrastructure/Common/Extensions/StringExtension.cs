using OneZero.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OneZero.Common.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 转换成Guid，转换异常则抛出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ConvertToGuid(this string str,string msg=null)
        {
            Guid guid;
            if (!Guid.TryParse(str, out guid))
            {
                throw new OneZeroException($"{msg}字符{str??" "}转Guid失败，格式不匹配",Enums.ResponseCode.ExpectedException);
            }
            return guid;
        }

        /// <summary>
        /// 转换成可空Guid?，转换异常则抛出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid? ConvertToNullableGuid(this string str, string msg = null)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            Guid guid;
            if (!Guid.TryParse(str, out guid))
            {
                throw new OneZeroException($"{msg}字符{str ?? " "}转Guid失败，格式不匹配", Enums.ResponseCode.ExpectedException);
            }
            return guid;
        }

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
                catch 
                {
                    return "";
                }
            }
        }
    }
}
