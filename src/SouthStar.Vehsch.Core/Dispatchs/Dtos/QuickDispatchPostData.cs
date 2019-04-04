using OneZero.Dtos;
using SouthStar.VehSch.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthStar.VehSch.Core.Dispatchs.Dtos
{
    public class QuickDispatchPostData:DataDto
    {
        /// <summary>
        /// 申请人ID
        /// </summary>
        public Guid ApplicantId { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// 用车单位ID
        /// </summary>       
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 用车单位名称
        /// </summary>      
        public string DepartmentName { get; set; }

        /// <summary>
        /// 用车人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用车人电话
        /// </summary>
        public string UserMobile { get; set; }

        /// <summary>
        /// 用车事由
        /// </summary>
        public string ApplyReson { get; set; }

        /// <summary>
        /// 使用人数
        /// </summary>     
        public string UserCount { get; set; }

        /// <summary>
        /// 车辆种类
        /// </summary>    
        public string CarType { get; set; }

        /// <summary>
        /// 出发地
        /// </summary>
        public string StartPoint { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        public CarProperty CarProperty { get; set; }

        /// <summary>
        /// 预计开始时间
        /// </summary>   
        public DateTime StartPlanTime { get; set; }

        /// <summary>
        /// 预计回队时间
        /// </summary>      
        public DateTime BackPlanTime { get; set; }

        /// <summary>
        /// 用车时长（字符串存备注）
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 司机ID
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// 司机姓名
        /// </summary>
        public string DriverName { get; set; }
        
        /// <summary>
        /// 司机电话
        /// </summary>
        public string DriverPhone { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public Guid VehicleId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }
    }
}
