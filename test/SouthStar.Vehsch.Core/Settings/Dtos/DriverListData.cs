using OneZero.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Setting.Dtos
{
    public class DriverListData:DataDto
    {
        /// <summary>
        /// 驾驶员Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentId { get; set; }


        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNum { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 驾驶证编号
        /// </summary>
        public string DrivingLicNum { get; set; }

        /// <summary>
        /// 初次领证日期
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// 准驾车型
        /// </summary>
        public string PermittedType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
