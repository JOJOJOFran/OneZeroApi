using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneZero.Application.Models;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Models
{
    /// <summary>
    /// 用车申请
    /// </summary>
    public class VehicleApplications : BaseEntity<Guid>
    {


        /// <summary>
        /// 申请编号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ApplyNum { get; set; }

        /// <summary>
        /// 申请人ID
        /// </summary>
        [Required]
        public int ApplicantId { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        [MaxLength(100)]
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申请人电话
        /// </summary>
        [MaxLength(30)]
        public string ApplicantPhone { get; set; }

        /// <summary>
        /// 用车单位ID
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 用车单位名称
        /// </summary>
        [MaxLength(500)]
        public string DepartmentName { get; set; }


        /// <summary>
        /// 用车人姓名
        /// </summary>
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// 用车人电话
        /// </summary>
        [MaxLength(50)]
        public string UserMobile { get; set; }

        /// <summary>
        /// 用车人职称
        /// </summary>
        [MaxLength(50)]
        public string UserTitle { get; set; }

        /// <summary>
        /// 使用人数
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string UserCount { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CarType { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CarProperty { get; set; }

        /// <summary>
        /// 使用区域
        /// </summary>
        [MaxLength(100)]
        public string UseArea { get; set; }

        /// <summary>
        /// 出发地点
        /// </summary>
        [MaxLength(200)]
        public string StartPoint { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        [MaxLength(200)]
        public string Destination { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        [MaxLength(500)]
        public string ApplyReson { get; set; }

        /// <summary>
        /// 预计开始时间
        /// </summary>
        public DateTime StartPlanTime { get; set; }

        /// <summary>
        /// 预计回队时间
        /// </summary>
        public DateTime BackPlanTime { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(100)]
        public string FileName { get; set; }

        /// <summary>
        /// 推送人
        /// </summary>
        public int? ReciverId { get; set; }

        /// <summary>
        /// 推送电话
        /// </summary>
        [MaxLength(50)]
        public string ReciverMobile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

    }

}
