using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneZero.Api.Model.Log
{
    /// <summary>
    /// 审计
    /// </summary>
    public class AuditModel : IEquatable<HashEntry>
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string MoudleName { get; set; }

        /// <summary>
        /// 页面名称
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OpretionTime { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 客户端
        /// </summary>
        public string ClinetName { get; set; }

        /// <summary>
        /// 请求IP（客户端）
        /// </summary>
        public string RequestIP { get; set; }

        /// <summary>
        /// 请求的URL(服务端)
        /// </summary>
        public string RequestUrl { get; set; }

        bool IEquatable<HashEntry>.Equals(HashEntry other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public static explicit operator HashEntry(AuditModel v)
        {
            return new HashEntry("1","2");
        }
    }
}
