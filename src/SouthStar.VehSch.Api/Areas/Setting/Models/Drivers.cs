using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneZero.Application.Models;

namespace SouthStar.VehSch.Api.Areas.Setting.Models
{
    public class Drivers : BaseEntity<Guid>
    {

        /// <summary>
        /// 部门ID
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(10)]
        public string Sex { get; set; }


        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [MaxLength(20)]
        public string PhoneNum { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string MobileNum { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// 驾驶证编号
        /// </summary>
        [Required]
        [MaxLength(50)]
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
        [MaxLength(30)]
        public string PermittedType { get; set; }

        /// <summary>
        /// 驾照类型
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string DrivingLicType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [MaxLength(10)]
        public string Status { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [MaxLength(50)]
        public string Image { get; set; }
    }
}
