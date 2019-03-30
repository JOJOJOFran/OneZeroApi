using Newtonsoft.Json;
using OneZero.Common.Convert;
using OneZero.Common.Dtos;
using SouthStar.VehSch.Api.Areas.ApplicationFlow.Models.Enum;
using SouthStar.VehSch.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthStar.VehSch.Api.Areas.ApplicationFlow.Dtos
{
    public class VehicleApplicationData :DataDto
    {
        /// <summary>
        /// 申请ID
        /// </summary>       
        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid Id { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplyNum { get; set; }


        /// <summary>
        /// 申请人ID
        /// </summary>
        [JsonConverter(typeof(GuidJsonConvert))]
        public Guid ApplicantId { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>       
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申请人电话
        /// </summary>
        public string ApplicantPhone { get; set; }

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
        /// 用车人职称
        /// </summary>
        public string UserTitle { get; set; }

        /// <summary>
        /// 使用人数
        /// </summary>     
        public string UserCount { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>    
        public string CarType { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        public CarProperty CarProperty { get; set; }

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
        /// 附件
        /// </summary>      
        public string FileName { get; set; }

        /// <summary>
        /// 推送人
        /// </summary>
        public Guid? ReciverId { get; set; }

        /// <summary>
        /// 推送电话
        /// </summary>
        public string ReciverMobile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>      
        public ApplyState Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
