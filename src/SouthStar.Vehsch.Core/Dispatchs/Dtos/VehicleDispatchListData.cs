using OneZero.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Core.Dispatch.Dtos
{
    public class VehicleDispatchListData:DataDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? Id { get; set; }

        /// 申请ID
        /// </summary>       
        public Guid ApplyId { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
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
        /// 用车单位名称
        /// </summary>      
        public string DepartmentName { get; set; }

        /// <summary>
        /// 用车人姓名
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
        /// 用车人电话
        /// </summary>
        public string UserMobile { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>    
        public string CarType { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        public string CarProperty { get; set; }

        /// <summary>
        /// 使用区域
        /// </summary>
        public string UseArea { get; set; }

        /// <summary>
        /// 出发地点
        /// </summary>
        public string StartPoint { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>  
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
        /// 申请状态
        /// </summary>      
        public string Status { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>      
        public string CheckStatus { get; set; }


        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 驾驶员电话
        /// </summary>
        public string DriverPhone { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
