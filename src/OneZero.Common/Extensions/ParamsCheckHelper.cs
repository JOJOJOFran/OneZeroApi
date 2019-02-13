using OneZero.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Common.Extensions
{
    public static class ParamsCheckHelper
    {
        public static void NotNull<T>(this T item)
        {
            if(item == null)
                throw new OneZeroException(typeof(T).Name+":参数为空");
        }

        public static void NotNull<T>(this T param, T value, string paramName)
        {
            Require<ArgumentNullException>(value != null, $"参数{paramName}不允许未空！");
        }

        /// <summary>
        /// 验证指定值的断言<paramref name="assertion"/>是否为真，如果不为真，抛出指定消息<paramref name="message"/>的指定类型<typeparamref name="TException"/>异常
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="assertion">要验证的断言。</param>
        /// <param name="message">异常消息。</param>
        private static void Require<TException>(bool assertion, string message)
            where TException : Exception
        {
            if (assertion)
            {
                return;
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            TException exception = (TException)Activator.CreateInstance(typeof(TException), message);
            throw exception;
        }
    }
}
