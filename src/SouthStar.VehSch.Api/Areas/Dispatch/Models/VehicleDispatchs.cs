using OneZero.Application.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SouthStar.VehSch.Api.Areas.Dispatch.Models.Enums;
using SouthStar.VehSch.Api.Common.Enums;

namespace SouthStar.VehSch.Api.Areas.Dispatch.Models
{
    public class VehicleDispatchs : BaseEntity<Guid>
    {

        #region 申请信息
        /// <summary>
        /// 申请单Id
        /// </summary>
        public Guid ApplyId { get; set; }

        /// <summary>
        /// 申请单编号
        /// </summary>
        [MaxLength(100)]
        public string ApplyNum { get; set; }


        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public string ApplicantPhone { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 使用人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 使用人数
        /// </summary>
        public string UserCount { get; set; }

        /// <summary>
        /// 使用人职位
        /// </summary>
        public string UserTitle { get; set; }

        /// <summary>
        /// 使用人电话
        /// </summary>
        public string UserMobile { get; set; }

        /// <summary>
        /// 使用人部门
        /// </summary>
        public string UserDepartment { get; set; }

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
        /// 用车类型
        /// </summary>
        [MaxLength(50)]
        public string CarType { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        public CarProperty CarProperty { get; set; }
        #endregion


        #region 派车信息
        /// <summary>
        /// 驾驶员Id
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 驾驶员电话
        /// </summary>
        public string DriverPhone { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid VehcileId { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 派车方式
        /// 0：自动派车  1：手动派车
        /// </summary>
        public DispatchType DispatchType { get; set; }

        /// <summary>
        /// 派车队列编号
        /// 自动派车的时候带入
        /// </summary>
        public int QueueNo { get; set; }
        #endregion
    }
}
