using OneZero.Common.Extensions;
using OneZero.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero
{
    /// <summary>
    /// 业务上下文
    /// </summary>
    public class OneZeroContext
    {

        /// <summary>
        /// 是否开启验证
        /// </summary>
        public bool IsAuththentic { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid TenanId { get; set; }

        /// <summary>
        /// 请求用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 请求用户角色
        /// </summary>
        public IEnumerable<string> RoleList { get; set; }

        /// <summary>
        /// 请求用户权限
        /// </summary>
        public IEnumerable<string> PermissionList { get; set; }

        /// <summary>
        /// 请求用户菜单
        /// </summary>
        public IEnumerable<string> MenuList { get; set; }

        /// <summary>
        /// 请求IP
        /// </summary>
        public string RequestIP { get; set; }

        /// <summary>
        /// 当前token ip
        /// </summary>
        public string TokenIP { get; set; }


        /// <summary>
        /// 请求路径
        /// </summary>
        public string ActionPath { get; set; }
    }
}
